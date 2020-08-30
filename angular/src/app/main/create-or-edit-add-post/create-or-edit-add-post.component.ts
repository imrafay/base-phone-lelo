import { Component, OnInit, Injector } from '@angular/core';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { AppComponentBase } from '@shared/app-component-base';
import { DropdownOutputDto, RoleDto, UserLocationServiceProxy, UserServiceProxy, ProductAdvertDto, ProductCompanyServiceProxy, ProductModelServiceProxy, ProductAdvertServiceProxy, ProductAdvertInputDto, ProductAdvertAccessoryDto, ProductAdvertImageDto, ProductAdvertDetailViewDto } from '@shared/service-proxies/service-proxies';
import { AppConsts } from '@shared/AppConsts';
import { HttpErrorResponse } from '@angular/common/http';
import { SelectItem, MenuItem } from 'primeng/api';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-create-or-edit-add-post',
  templateUrl: './create-or-edit-add-post.component.html',
  styleUrls: ['./create-or-edit-add-post.component.css']
})
export class CreateOrEditAddPostComponent extends AppComponentBase implements OnInit {

  states: DropdownOutputDto[] = [];
  roles: RoleDto[] = [];
  city: DropdownOutputDto[] = [];
  neighbourhood: DropdownOutputDto[] = [];
  selectedState: DropdownOutputDto[] = [];
  uploadUrl: string;
  baseUrl = AppConsts.remoteServiceBaseUrl;
  fileInQue: boolean = false;
  uploadedFiles: any[] = [];
  // productBrands: DropdownOutputDto[] = [];
  productBrands = [];
  selectedProductBrands: DropdownOutputDto[] = [];
  productModel = [];
  // selectedProductModel=[];
  selectedProductModel: DropdownOutputDto[] = [];
  // accessories:ProductAdvertAccessoryDto[] = [];;
  selectedAccessories: ProductAdvertAccessoryDto[] = [];
  isNegotiable: any
  isFixed: boolean = false;
  ram = [];
  selectedRam: DropdownOutputDto[] = [];
  storage = [];
  selectedStorage: DropdownOutputDto[] = [];;
  isSpot: any;
  isNew: any;
  isExchange: any;
  isPtaApproved: any;
  isDamaged: any;
  product: ProductAdvertInputDto = new ProductAdvertInputDto();
  phoneTitle: string = ''
  price: number;
  isFingerWorking: any
  isFaceSensorWorking: any;
  batteryHealth: number;
  isKit: any;
  isWarranty: any;
  warrantyMonths: number;
  rangeValues: number[] = [0, 5000];
  selectedBatteryWifi
  selectedBatteryGaming
  selectedBatteryMobileData
  selectedBattery = []
  detailProduct: any;
  description: string = '';
  productId: number;

  constructor(
    injector: Injector,
    private _authService: AppAuthService,
    private _UserLocationServiceProxy: UserLocationServiceProxy,
    private _ProductCompanyService: ProductCompanyServiceProxy,
    private _ProductModelService: ProductModelServiceProxy,
    private _ProductAdvertService: ProductAdvertServiceProxy,
    public route: ActivatedRoute, private router: Router,

  ) {
    super(injector);
    this.uploadUrl = this.baseUrl + "api/File/UploadFiles";
    this.detailProduct = this.router.getCurrentNavigation().extras.state;
    console.log(this.detailProduct);
    // if()
    console.log(this.appSession)
  }


  pricesBargaining = [
    { name: 'Negotiable', id: 1 },
    { name: 'Fixed', id: 2 },
  ]

  // condition = [
  //   { name: 'New', id: 1 },
  //   { name: 'Used', id: 2 },
  // ]
  booleanEnum = [
    { name: 'Yes', id: 1 },
    { name: 'No', id: 2 },
  ]
  // ptaStatus = [
  //   { name: 'approved', id: 1 },
  //   { name: 'non-approved', id: 2 },
  // ]
  batteryWifi = [
    { name: '5', id: 1, hours: 5, batteryUsageType: 1 },
    { name: '7', id: 2, hours: 7, batteryUsageType: 1 },
    { name: '10', id: 3, hours: 10, batteryUsageType: 1 },
    { name: '12+', id: 4, hours: 12, batteryUsageType: 1 },
  ]
  batteryGaming = [
    { name: '5', id: 1, hours: 5, batteryUsageType: 2 },
    { name: '7', id: 2, hours: 7, batteryUsageType: 2 },
    { name: '10', id: 3, hours: 10, batteryUsageType: 2 },
    { name: '12+', id: 4, hours: 12, batteryUsageType: 2 },
  ]
  batteryMobileData = [
    { name: '5', id: 1, hours: 5, batteryUsageType: 3 },
    { name: '7', id: 2, hours: 7, batteryUsageType: 3 },
    { name: '10', id: 3, hours: 10, batteryUsageType: 3 },
    { name: '12+', id: 4, hours: 12, batteryUsageType: 3 },
  ]
  accessories = [
    { id: 0, accessoryName: "Charger", accessoryType: 0 },
    { id: 1, accessoryName: "Wireless Charger", accessoryType: 1 },
    { id: 2, accessoryName: "Box", accessoryType: 2 },
    { id: 3, accessoryName: "HandsFree", accessoryType: 3 },
    { id: 4, accessoryName: "AirPods", accessoryType: 4 },
  ]


  ngOnInit(): void {
    this.getAllRams();
    this.getAllStorages();
    // this.getAllAccessories();
    this.getAllBrands();

    this.detailProduct ? this.getProductById() : null;

  }
  getProductById() {
    this._ProductAdvertService.getProductAdverForEdit(this.detailProduct.productdetails.queryParams.id).subscribe(res => {
      console.log(res)
      this.selectedProductBrands = this.productBrands.filter(response => response.name == res.productCompanyName)[0]
      setTimeout(() => this.onSelectBrand(null, this.selectedProductBrands['id']), 1000);
      setTimeout(() => this.selectedProductModel = this.productModel.filter(response => response.name == res.productModelName)[0], 6000);

      //  setTimeout(() => {

      // this.accessories.filter(response => {
      //    res.productAdvertAccessories.map(acc => {
      //      acc.id = 3;
      //     if(response.id == acc.id) this.selectedAccessories = response

      //     })
      //   })

      // }, 2000);

      this.isNew = res.productAdvert.isNew == true ? this.booleanEnum[0] : this.booleanEnum[1];
      this.isExchange = res.productAdvert.isExchangeable == true ? this.booleanEnum[0] : this.booleanEnum[1];
      this.isPtaApproved = res.productAdvert.isPtaApproved == true ? this.booleanEnum[0] : this.booleanEnum[1];
      this.isNegotiable = res.productAdvert.isNegotiable == true ? this.pricesBargaining[0] : this.pricesBargaining[1];
      this.price = res.productAdvert.price;
      this.isSpot = res.productAdvert.isSpot == true ? this.booleanEnum[0] : this.booleanEnum[1];
      this.description = res.productAdvert.description;
      this.isFingerWorking = res.productAdvert.isFingerSensorWorking == true ? this.booleanEnum[0] : this.booleanEnum[1];
      this.isFaceSensorWorking = res.productAdvert.isFaceSensorWorking == true ? this.booleanEnum[0] : this.booleanEnum[1];
      this.isKit = res.productAdvert.isKit == true ? this.booleanEnum[0] : this.booleanEnum[1];
      this.isWarranty = res.productAdvert.isInWarranty == true ? this.booleanEnum[0] : this.booleanEnum[1];
      this.isDamaged = res.productAdvert.isDamage == true ? this.booleanEnum[0] : (res.productAdvert.isDamage == false ? this.booleanEnum[1] : null);
      this.selectedRam = this.ram.filter(response => response.id == res.productAdvert.ram)[0];
      this.selectedStorage = this.storage.filter(response => response.id == res.productAdvert.storage)[0];
      this.rangeValues[0] = res.productAdvert.negotiableMinValue
      this.rangeValues[1] = res.productAdvert.negotiableMaxValue

      this.batteryWifi.filter(bat =>
        res.productAdvertBatteryUsages.map(x => {
          if (bat.hours == x.hours && bat.batteryUsageType == x.batteryUsageType) this.selectedBatteryWifi = bat;
        }))


      this.batteryGaming.filter(bat =>
        res.productAdvertBatteryUsages.map(x => {
          if (bat.hours == x.hours && bat.batteryUsageType == x.batteryUsageType) this.selectedBatteryGaming = bat;
        }))

      this.batteryMobileData.filter(bat =>
        res.productAdvertBatteryUsages.map(x => {
          if (bat.hours == x.hours && bat.batteryUsageType == x.batteryUsageType) this.selectedBatteryMobileData = bat;
        }))

    })

  }

  s = (ev) => console.log(this.isNew);
  onSelectDropdown(valuesArray): void {
    console.log(valuesArray)
    if (valuesArray) {
      // this.selectedState = valuesArray[0];
      // this.selectedCity = valuesArray[1];
      // this.selectedNeighbourhood = valuesArray[2];
      // this.neighbourhood = valuesArray[3];
    }

    // this.isValid(valuesArray)
  }
  handleChange(e) {
    console.log(e)
  }
  getAllStates() {

    this._UserLocationServiceProxy.getStates().subscribe(res => {
      this.states = res;
      console.log(this.states)

    })
    console.log(this.city)
  }
  getAllCities() {

    this._UserLocationServiceProxy.getCitiesByStateId(4).subscribe(res => {
      this.city = res;

    })
  }


  onUpload(event): void {
    console.log(event);
    if (event.files.length > 0) {
      const originalEvent = event.originalEvent;
      if (originalEvent.body.success) {
        for (let file of event.files) {
          this.uploadedFiles.push(file);
        }
        console.log(originalEvent.body.result[0].fileName);
        this.notify.success(this.l("File Uploaded"));

      } else {

        console.log(originalEvent.body.error.message);

      }
    }
  }
  onSelect(event) {
    this.fileInQue = true;

  }
  onRemove(event) {
    this.fileInQue = false;

  }
  onBeforeUpload(event): void {
    console.log("event", event);
    event.formData.append("PhoneLeloDataFileType", 1);
  }
  getAllBrands() {
    this._ProductCompanyService.getProductCompanyDropdown().subscribe(res => {
      this.productBrands = res;
    })
  }
  getAllProductsDropdownById() {
    this._ProductModelService.getProductModelDropdown(this.selectedProductBrands['id']).subscribe(res => {
      this.productModel = res;
    })
  }


  onSelectBrand(e, id?) {
    console.log(e)
    console.log(this.selectedProductBrands)
    // e.value.id || id ? this.getAllProductsDropdownById() : null
    this.getAllProductsDropdownById()
    console.log(this.selectedAccessories)
  }


  onSelectSpot(e) {
    e.target.value == '1' ? this.isSpot = true : this.isSpot = false
    e.target.value == '2' ? this.isSpot = false : this.isSpot = true
  }
  onError(event) {
    console.log(event);
    let error: HttpErrorResponse = event.error;
    if (error) {
      console.log(error)
    }
  }
  logout(): void {
    this._authService.logout();
  }
  getAllRams() {
    console.log(this.productId)
    this._ProductAdvertService.getRamDropDown().subscribe(res => {
      this.ram = res;
    })
  }
  // getAllAccessories() {
  //   this._ProductAdvertService.getAccessoriesDropDown().subscribe((res:DropdownOutputDto[]) => {
  //     this.accessories = res;
  //   })
  // }
  getAllStorages() {
    this._ProductAdvertService.getStorageDropDown().subscribe(res => {
      this.storage = res;
    })
  }

  isValid() {

    return !(
      this.phoneTitle != '' && this.selectedProductBrands['id'] && this.selectedProductModel['id'] &&
        this.isWarranty == 1 ? this.warrantyMonths : this.isWarranty == 2 &&
        this.isNew && this.isExchange && this.isPtaApproved && this.price && this.isSpot &&
        this.isFaceSensorWorking && this.isFingerWorking && this.selectedRam['id'] && this.selectedStorage['id'] &&
        this.isWarranty && this.isKit && (this.selectedProductBrands['name'] == 'Apple' ? this.batteryHealth : this.selectedProductBrands['name'])
    )
    // && this.selectedProductBrands['name'] == AppConsts.mobileBrand.Apple ? this.batteryHealth : this.selectedProductBrands[/'']
  }

  onPostAd() {

    this.product.productAdvertAccessories = this.selectedAccessories;


    let selectedBatteryWifi = this.batteryWifi.filter(res => res.id == this.selectedBatteryWifi['id'])[0]
    let selectedBatteryGaming = this.batteryGaming.filter(res => res.id == this.selectedBatteryGaming['id'])[0]
    let selectedBatteryMobileData = this.batteryMobileData.filter(res => res.id == this.selectedBatteryMobileData['id'])[0]

    // this.product.productAdvertAccessories = this.selectedAccessories;


    this.selectedBattery.push(selectedBatteryWifi,
      selectedBatteryGaming, selectedBatteryMobileData)
    console.log(this.selectedBattery)


    this.product.productAdvertBatteryUsages = this.selectedBattery;
    this.product.productAdvertAccessories = this.selectedBattery;

    this.product.images = []
    console.log(this.selectedAccessories)
    this.product.productAdvertinput = new ProductAdvertDto();
    this.product.productAdvertinput.description = this.description;
    this.product.productAdvertinput.productModelId = this.selectedProductModel['id'];
    this.product.productAdvertinput.storage = this.selectedStorage['id'];
    this.product.productAdvertinput.ram = this.selectedRam['id'];
    this.product.productAdvertinput.isNew = this.isNew['id'] == 1 ? true : false
    this.product.productAdvertinput.isPtaApproved = this.isPtaApproved['id'] == 1 ? true : false
    this.product.productAdvertinput.isExchangeable = this.isExchange['id'] == 1 ? true : false
    this.product.productAdvertinput.price = this.price
    this.product.productAdvertinput.isNegotiable = this.isNegotiable['id'] == 1 ? true : false
    this.product.productAdvertinput.negotiableMaxValue = this.isNegotiable['id'] == 1 ? this.rangeValues[0] : null;
    this.product.productAdvertinput.negotiableMinValue = this.isNegotiable['id'] == 1 ? this.rangeValues[1] : null;
    this.product.productAdvertinput.isSpot = this.isSpot == 1 ? true : false;
    this.product.productAdvertinput.isFingerSensorWorking = this.isFingerWorking['id'] == 1 ? true : false;
    this.product.productAdvertinput.isFaceSensorWorking = this.isFaceSensorWorking['id'] == 1 ? true : false;
    this.product.productAdvertinput.batteryHealth = this.selectedProductBrands['name'] == AppConsts.mobileBrand.Apple ? this.batteryHealth : null
    this.product.productAdvertinput.isKit = this.isKit['id'] == 1 ? true : false;
    this.product.productAdvertinput.isInWarranty = this.isWarranty['id'] == 1 ? true : false;
    this.product.productAdvertinput.remaingWarrantyInMonths = this.isWarranty['id'] == 1 ? this.warrantyMonths : null;
    this.detailProduct ? this.product.productAdvertinput.id = this.detailProduct.productdetails.queryParams.id : null
    console.log(this.product)
    console.log(this.product.productAdvertAccessories)



    !this.detailProduct ? this._ProductAdvertService.create(this.product).subscribe(res => {
      console.log(res)
      this.notify.success(this.l('Successfully Created'));
      setTimeout(() => this.router.navigate(['/app/main/user-dashboard']), 1000);


    }) : this._ProductAdvertService.update(this.product).subscribe(res => {
      console.log(res)
      this.notify.success(this.l('Successfully Updated'));
      setTimeout(() => this.router.navigate(['/app/main/user-dashboard']), 1000);


    })

  }


  suggestion(text: string) {
    this.description = this.description + text + '.'
    console.log(this.description)
  }

  reset = () => this.description = ''
}
