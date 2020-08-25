import { Component, OnInit, Injector } from '@angular/core';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { AppComponentBase } from '@shared/app-component-base';
import { DropdownOutputDto, RoleDto, UserServiceProxy, ProductCompanyServiceProxy, ProductModelServiceProxy } from '@shared/service-proxies/service-proxies';
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
  accessories: SelectItem[];
  selectedAccessories: SelectItem[];
  isNegotiable: boolean = false;
  isFixed: boolean = false;
  constructor(
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private _authService: AppAuthService,
    private _UserServiceProxy: UserServiceProxy,
    private _ProductCompanyService: ProductCompanyServiceProxy,
    private _ProductModelService: ProductModelServiceProxy

  ) {
    super(injector);
    this.uploadUrl = this.baseUrl + "api/File/UploadFiles";


    console.log(this.appSession)

  }
  items: MenuItem[];
  activeItem: MenuItem;
  pricesBargaining = [
    { name: 'Negotiable', id: 1 },
    { name: 'Fixed', id: 2 },
  ]

  batteryWifi = [
    { name: '5-7', id: 1 },
    { name: '7-10', id: 2 },
    { name: '10-12', id: 3 },
    { name: '15+', id: 4 },
  ]
  batteryGaming = [
    { name: '5-7', id: 1 },
    { name: '7-10', id: 2 },
    { name: '10-12', id: 3 },
    { name: '15+', id: 4 },
  ]
  batteryMobileData = [
    { name: '5-7', id: 1 },
    { name: '7-10', id: 2 },
    { name: '10-12', id: 3 },
    { name: '15+', id: 4 },
  ]
  ram = [
    { name: '2', id: 1 },
    { name: '3', id: 2 },
    { name: '4', id: 3 },
    { name: '6', id: 4 },
    { name: '8', id: 5 },
    { name: '9', id: 6 },
    { name: '10', id: 7 },
    { name: '12', id: 8 },
    { name: '12+', id: 9 },
  ]
  storage = [
    { name: '16gb', id: 1 },
    { name: '32gb', id: 2 },
    { name: '64gb', id: 3 },
    { name: '128gb', id: 4 },
    { name: '256gb', id: 5 },
    { name: '512gb', id: 6 },
    { name: '1tb', id: 7 },

  ]
  ngOnInit(): void {
    this.getAllCities();
    this.getAllStates();
    this.getAllBrands();
    this.items = [
      { label: 'Home', icon: 'pi pi-fw pi-home' },
      { label: 'Calendar', icon: 'pi pi-fw pi-calendar' },
      { label: 'Edit', icon: 'pi pi-fw pi-pencil' },
      { label: 'Documentation', icon: 'pi pi-fw pi-file' },
      { label: 'Settings', icon: 'pi pi-fw pi-cog' }
    ];
    this.activeItem = this.items[0];

    this.accessories = [
      { label: 'charger', value: { id: 1, name: 'charger' } },
      { label: 'handfree', value: { id: 2, name: 'handfree' } },
      { label: 'box', value: { id: 3, name: 'box' } },
      { label: 'charger dock', value: { id: 4, name: 'chargerDock' } },
    ];
  }
  rangeValues: number[] = [0, 5000];

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

    this._UserServiceProxy.getStates().subscribe(res => {
      this.states = res;
      console.log(this.states)

    })
    console.log(this.city)
  }
  getAllCities() {

    this._UserServiceProxy.getCitiesByStateId(4).subscribe(res => {
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
    e.value.id ? this.getAllProductsById() : null
    console.log(this.selectedAccessories)
  }
  onSelectNegotiable(e) {
    console.log(e)
    e.target.value == '1' ? this.isNegotiable = true : this.isNegotiable = false
    e.target.value == '2' ? this.isFixed = true : this.isFixed = false
    console.log(this.isNegotiable)
    console.log(this.isFixed)

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

}
