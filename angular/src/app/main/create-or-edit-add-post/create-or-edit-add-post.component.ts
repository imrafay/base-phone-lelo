import { Component, OnInit, Injector } from '@angular/core';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { AppComponentBase } from '@shared/app-component-base';
import { DropdownOutputDto, RoleDto, UserLocationServiceProxy, UserServiceProxy, ProductAdvertDto, ProductCompanyServiceProxy, ProductModelServiceProxy, ProductAdvertServiceProxy, ProductAdvertInputDto, ProductAdvertAccessoryDto, ProductAdvertImageDto } from '@shared/service-proxies/service-proxies';
import { AppConsts } from '@shared/AppConsts';
import { HttpErrorResponse } from '@angular/common/http';
import { SelectItem, MenuItem } from 'primeng/api';

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
  productBrands: DropdownOutputDto[] = [];
  selectedProductBrands: DropdownOutputDto[] = [];
  productModel: DropdownOutputDto[] = [];
  selectedProductModel: DropdownOutputDto[] = [];
  accessories: DropdownOutputDto[] = [];
  selectedAccessories = [];
  isNegotiable: number;
  isFixed: boolean = false;
  ram: DropdownOutputDto[] = [];
  selectedRam: DropdownOutputDto[] = [];
  storage: DropdownOutputDto[] = [];
  selectedStorage: DropdownOutputDto[] = [];
  isSpot: any;
  isNew: any;
  isExchange: any;
  isPtaApproved: any;
  product: ProductAdvertInputDto = new ProductAdvertInputDto();
  phoneTitle: string = ''
  price: number;
  isFingerWorking: number
  isFaceSensorWorking: number;
  batteryHealth: number;
  isKit: number;
  isWarranty: number;
  warrantyMonths: number;
  rangeValues: number[] = [0, 5000];
  selectedBatteryWifi
  selectedBatteryGaming
  selectedBatteryMobileData
  selectedBattery = []


  constructor(
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private _authService: AppAuthService,
    private _UserServiceProxy: UserServiceProxy,
    private _UserLocationServiceProxy: UserLocationServiceProxy,
    private _ProductCompanyService: ProductCompanyServiceProxy,
    private _ProductModelService: ProductModelServiceProxy,
    private _ProductAdvertService: ProductAdvertServiceProxy

  ) {
    super(injector);
    this.uploadUrl = this.baseUrl + "api/File/UploadFiles";

    console.log(this.appSession)

  }


  pricesBargaining = [
    { name: 'Negotiable', id: 1 },
    { name: 'Fixed', id: 2 },
  ]

  condition = [
    { name: 'New', id: 1 },
    { name: 'Used', id: 2 },
  ]
  booleanEnum = [
    { name: 'Yes', id: 1 },
    { name: 'No', id: 2 },
  ]
  ssss = [
    { name: 'Yes', id: 1 },
    { name: 'No', id: 2 },
  ]
  ptaStatus = [
    { name: 'approved', id: 1 },
    { name: 'non-approved', id: 2 },
  ]
  batteryWifi = [
    { name: '5', id: 1, hours: 5 },
    { name: '7', id: 2, hours: 7 },
    { name: '10', id: 3, hours: 10 },
    { name: '12+', id: 4, hours: 12 },
  ]
  batteryGaming = [
    { name: '5', id: 1, hours: 5 },
    { name: '7', id: 2, hours: 7 },
    { name: '10', id: 3, hours: 10 },
    { name: '12+', id: 4, hours: 12 },
  ]
  batteryMobileData = [
    { name: '5', id: 1, hours: 5 },
    { name: '7', id: 2, hours: 7 },
    { name: '10', id: 3, hours: 10 },
    { name: '12+', id: 4, hours: 12 },
  ]


  ngOnInit(): void {
    this.getAllRams();
    this.getAllStorages();
    this.getAllAccessories();
    this.getAllBrands();

  }

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
  getAllProductsById() {
    this._ProductModelService.getProductModelDropdown(this.selectedProductBrands['id']).subscribe(res => {
      this.productModel = res;
    })
  }


  onSelectProduct(e) {
    console.log(e)
  }

  onSelectBrand(e) {
    console.log(e)
    console.log(this.selectedProductBrands)
    e.value.id ? this.getAllProductsById() : null
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
    this._ProductAdvertService.getRamDropDown().subscribe(res => {
      this.ram = res;
    })
  }
  getAllAccessories() {
    this._ProductAdvertService.getAccessoriesDropDown().subscribe(res => {
      this.accessories = res;
    })
  }
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
  onSelectAccessories(ev) {
    console.log(ev)
    this.selectedAccessories = ev.value
    console.log(this.selectedAccessories)

  }
  onPostAd() {

    this.selectedAccessories.map((res: any) => {
      console.log(res)
      res.accessoryName = res.name
      res.accessoryType = 1
      res.id = res.id
    })
    this.product.productAdvertAccessories = this.selectedAccessories
    console.log(this.product.productAdvertAccessories)

    let selectedBatteryWifi = this.batteryWifi.filter(res => res.id == this.selectedBatteryWifi)[0]
    let selectedBatteryGaming = this.batteryGaming.filter(res => res.id == this.selectedBatteryGaming)[0]
    let selectedBatteryMobileData = this.batteryMobileData.filter(res => res.id == this.selectedBatteryMobileData)[0]


    this.selectedBattery.push(selectedBatteryWifi,
      selectedBatteryGaming, selectedBatteryMobileData)

    this.selectedBattery.map((res: any) => {
      console.log(res)
      res.hours = res.hours,
        res.batteryUsageType = 1
      res.id = res.id
    })

    this.product.productAdvertBatteryUsages = this.selectedBattery

    console.log(this.selectedAccessories)
    this.product.productAdvertinput = new ProductAdvertDto();
    this.product.productAdvertinput.productModelId = this.selectedProductModel['id'];
    this.product.productAdvertinput.storage = this.selectedStorage['id'];
    this.product.productAdvertinput.ram = this.selectedRam['id'];
    this.product.productAdvertinput.isNew = this.isNew == 1 ? true : false
    this.product.productAdvertinput.isPtaApproved = this.isPtaApproved == 1 ? true : false
    this.product.productAdvertinput.isExchangeable = this.isExchange == 1 ? true : false
    this.product.productAdvertinput.price = this.price
    this.product.productAdvertinput.isNegotiable = this.isNegotiable == 1 ? true : false
    this.product.productAdvertinput.negotiableMaxValue = this.isNegotiable == 1 ? this.rangeValues[0] : null;
    this.product.productAdvertinput.negotiableMinValue = this.isNegotiable == 1 ? this.rangeValues[1] : null;
    this.product.productAdvertinput.isSpot = this.isSpot == 1 ? true : false;
    this.product.productAdvertinput.isFingerSensorWorking = this.isFingerWorking == 1 ? true : false;
    this.product.productAdvertinput.isFaceSensorWorking = this.isFaceSensorWorking == 1 ? true : false;
    this.product.productAdvertinput.batteryHealth = this.selectedProductBrands['name'] == AppConsts.mobileBrand.Apple ? this.batteryHealth : null
    this.product.productAdvertinput.isKit = this.isKit == 1 ? true : false;
    this.product.productAdvertinput.isInWarranty = this.isWarranty == 1 ? true : false;
    this.product.productAdvertinput.remaingWarrantyInMonths = this.isWarranty == 1 ? this.warrantyMonths : null;

    this._ProductAdvertService.create(this.product).subscribe(res => {
      console.log(res)
    })

    console.log(this.product)
  }


  // this._ProductAdvertService.create(this.product.).subscribe(res=>{
  //       console.log(res)
  //     })
  //   }
}
