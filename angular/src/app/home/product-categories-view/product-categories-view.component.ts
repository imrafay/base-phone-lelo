import { Component, OnInit } from '@angular/core';
import { ProductCompanyServiceProxy, DropdownOutputDto, ProductAdvertServiceProxy, ProductAdvertViewDto } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute, ActivationStart, NavigationStart, Router } from '@angular/router';
import { isNull } from 'util';

@Component({
  selector: 'product-categories-view',
  templateUrl: './product-categories-view.component.html',
  styleUrls: ['./product-categories-view.component.css']
})
export class ProductCategoriesViewComponent implements OnInit {
  productBrands: DropdownOutputDto[] = [];
  productBrandsLength;
  ramLength;
  isShowRam = false;
  isShowstorage = false;
  highlightedRows = []
  products: ProductAdvertViewDto[] = [];
  productCompanyId: number;
  isProgress: boolean = false;
  productsLength: number;

  booleanEnum = [
    { name: 'Yes', id: 1 },
    { name: 'No', id: 2 },
  ]
  condition = [
    { name: 'New', id: 1 },
    { name: 'Used', id: 2 },
  ]
  isNew;
  isPTAapproved;
  rangeValues: number[] = [0, 400000];
  isExchangeable
  isSpot: DropdownOutputDto[] = [];
  ram: DropdownOutputDto[] = [];
  spliceStorage: DropdownOutputDto[] = [];
  spliceRam: DropdownOutputDto[] = [];
  selectedRam: DropdownOutputDto[] = [];
  storage: DropdownOutputDto[] = [];
  selectedStorage: DropdownOutputDto[] = [];
  searchFilter = '';
  isDamage;
  isNegotiable;
  ramSelected = [];
  storageSelected = [];

  constructor(
    private _ProductCompanyService: ProductCompanyServiceProxy,
    private _ProductAdvertService: ProductAdvertServiceProxy,
    private _route: ActivatedRoute,
    private router: Router,
  ) {
    console.log(this._route.params)
    this.searchFilter = this._route.params ? this._route.params['value'].id : '';
    this.router.events.subscribe((val) => {
      console.log(val);
      if (val instanceof ActivationStart) {
        if (val) {
          if (val.snapshot.url.length > 0) {
            this.searchFilter = val.snapshot.url[1].path;
          }
        }
        this.getAllProducts();
      }
    });
  }


  ngOnInit(): void {
    this.getAllBrands();
    this.getAllStorages();
    this.getAllProducts();
    this.getAllRams();
  }

  getAllBrands() {
    this._ProductCompanyService.getProductCompanyDropdown().subscribe(res => {
      this.productBrands = res;
      this.productBrandsLength = res.length;
      // if (this.productBrandsLength > 6) {
      //   setTimeout(() => {
      //     for (let index = 6; index < this.productBrandsLength + 1; index++) {
      //       document.getElementById('product-' + index).style.display = 'none';
      //     }
      //   }, 0);
      // }
    })
  }
  getAllProducts(sortEnum?) {
    console.log(this.selectedStorage['id'])
    this._ProductAdvertService.getAll(
      undefined, undefined, undefined, undefined, undefined,
      this.productCompanyId, this.searchFilter,
      this.selectedRam ? this.ramSelected : null,
      this.selectedStorage ? this.storageSelected : null,
      this.isNew ? (this.isNew['id'] == 1 ? true : (this.isNew['id'] == 2 ? false : null)) : null,
      this.isPTAapproved ? (this.isPTAapproved['id'] == 1 ? true : (this.isPTAapproved['id'] == 2 ? false : null)) : null,
      this.isExchangeable ? (this.isExchangeable['id'] == 1 ? true : (this.isExchangeable['id'] == 2 ? false : null)) : null,
      this.rangeValues ? this.rangeValues[0] : null,
      this.rangeValues ? this.rangeValues[1] : null,
      this.isNegotiable ? (this.isNegotiable['id'] == 1 ? true : (this.isNegotiable['id'] == 2 ? false : null)) : null,
      this.isSpot ? (this.isSpot['id'] == 1 ? true : (this.isSpot['id'] == 2 ? false : null)) : null,
      this.isDamage ? (this.isDamage['id'] == 1 ? true : (this.isDamage['id'] == 2 ? false : null)) : null,
      undefined, undefined,
      sortEnum ? sortEnum : undefined,
    ).subscribe(res => {
      this.isProgress = true;
      console.log(res)
      // this.isInProgress = true;
      this.products = res.items.slice(0, 4);
      this.productsLength = res.items.length;
    })
  }
  getAllRams() {
    this._ProductAdvertService.getRamDropDown().subscribe(res => {
      this.ram = res.slice(0, 4);
      this.spliceRam = res;
    })
  }
  getAllStorages() {
    this._ProductAdvertService.getStorageDropDown().subscribe(res => {
      this.storage = res.slice(0, 4);
      this.spliceStorage = res;
    })
  }

  showMoreBrands(text: string) {
    if (text == 'ram') {
      this.isShowRam = true;
      this.ram = [...this.spliceRam];
    }
    if (text == 'storage') {
      this.isShowstorage = true;
      this.storage = [...this.spliceStorage]
    }
  }

  showLessBrands(text: string) {
    if (text == 'ram') {
      this.isShowRam = false;
      this.ram = this.ram.splice(0, 4);
    }
    if (text == 'storage') {
      this.isShowstorage = false;
      this.storage = this.storage.splice(0, 4);
    }
  }

  onSelectBrand(id) {
    this.productCompanyId = id;
    this.isProgress = false;
    this.highlightedRows.length > 0 ? this.highlightedRows.pop() : null;
    this.highlightedRows.push(id);
    this.products = [];
    this.getAllProducts();
  }

  onSelectFilter(event?, sortEnum?) {

    if (this.selectedRam.length != 0) {
      this.ramSelected = [];
      this.ramSelected.push(this.selectedRam['id'])
    }
    if (this.selectedStorage.length != 0) {
      this.storageSelected = [];
      this.storageSelected.push(this.selectedStorage['id'])
    }

    console.log(this.rangeValues)
    console.log(this.selectedRam)
    console.log(this.selectedStorage)
    this.isProgress = false;
    this.getAllProducts(sortEnum ? sortEnum : null);
  }

  resetRightFilters() {
    this.isProgress = false;
    this.productCompanyId = null;
    this.highlightedRows = [];
    this.products = [];
    this.getAllProducts();
  }

  resetLeftFilters() {
    this.isProgress = false;
    this.isSpot = null;
    this.isNew = null;
    this.isPTAapproved = null;
    this.isExchangeable = null;
    this.isDamage = null;
    this.selectedStorage = null;
    this.selectedRam = null;
    this.isProgress = false;
    this.isNegotiable = false;
    this.rangeValues = [0, 400000];
    this.getAllProducts();

  }

}
