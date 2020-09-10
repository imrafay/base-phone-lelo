import { Component, OnInit } from '@angular/core';
import { ProductCompanyServiceProxy, DropdownOutputDto, ProductAdvertServiceProxy, ProductAdvertViewDto } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'product-categories-view',
  templateUrl: './product-categories-view.component.html',
  styleUrls: ['./product-categories-view.component.css']
})
export class ProductCategoriesViewComponent implements OnInit {
  productBrands: DropdownOutputDto[] = [];
  productBrandsLength;
  isShow = false;
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
  rangeValues: number[] = [0, 5000];
  isExchangeable
  isSpot: DropdownOutputDto[] = [];
  ram = [];
  selectedRam: DropdownOutputDto[] = [];
  storage = [];
  selectedStorage: DropdownOutputDto[] = [];
  searchFilter = '';
  isDamage
  isNegotiable
  constructor(
    private _ProductCompanyService: ProductCompanyServiceProxy,
    private _ProductAdvertService: ProductAdvertServiceProxy,
    private _route: ActivatedRoute


  ) {
    console.log(this._route.params)
    this.searchFilter = this._route.params ? this._route.params['value'].id : ''
  }

  ngOnInit(): void {
    this.getAllBrands();
    this.getAllRams();
    this.getAllStorages();
    this.getAllProducts();
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
  getAllProducts() {
    this._ProductAdvertService.getAll(
      undefined, undefined, undefined, undefined, undefined,
      this.productCompanyId, this.searchFilter,
      this.selectedRam ? this.selectedRam['id'] : null,
      this.selectedStorage ? this.selectedStorage['id'] : null,
      this.isNew ? (this.isNew['id'] == 1 ? true : (this.isNew['id'] == 2 ? false : null)) : null,
      this.isPTAapproved ? (this.isPTAapproved['id'] == 1 ? true : (this.isPTAapproved['id'] == 2 ? false : null)) : null,
      this.isExchangeable ? (this.isExchangeable['id'] == 1 ? true : (this.isExchangeable['id'] == 2 ? false : null)) : null,
      this.rangeValues ? this.rangeValues[0] : null,
      this.rangeValues ? this.rangeValues[0] : null,
      this.isNegotiable ? (this.isNegotiable['id'] == 1 ? true : (this.isNegotiable['id'] == 2 ? false : null)) : null,
      this.isSpot ? (this.isSpot['id'] == 1 ? true : (this.isSpot['id'] == 2 ? false : null)) : null,
      this.isDamage ? (this.isDamage['id'] == 1 ? true : (this.isDamage['id'] == 2 ? false : null)) : null,

      undefined, undefined, undefined,
    ).subscribe(res => {
      this.isProgress = true;
      console.log(res)
      // this.isInProgress = true;
      this.products = res.items;
      this.productsLength = res.items.length;

    })
  }
  getAllRams() {
    this._ProductAdvertService.getRamDropDown().subscribe(res => {
      this.ram = res;

    })
  }
  getAllStorages() {
    this._ProductAdvertService.getStorageDropDown().subscribe(res => {
      this.storage = res;
    })
  }

  showMoreBrands() {
    if (this.productBrandsLength > 6) {

      this.isShow = true;
      for (let index = 6; index < this.productBrandsLength + 1; index++) {
        document.getElementById('product-' + index).style.display = 'block';
      }
    }
  }

  showLessBrands() {
    if (this.productBrandsLength > 6) {

      this.isShow = false;

      for (let index = 6; index < this.productBrandsLength + 1; index++) {
        document.getElementById('product-' + index).style.display = 'none';
      }
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
  
  onSelectFilter(event) {
    console.log(this.rangeValues)
    this.isProgress = false;
    this.getAllProducts();
  }

  resetRightFilters() {
    this.isProgress = false;
    this.productCompanyId = null;
    this.highlightedRows = [];
    this.products = [];
    this.getAllProducts();
  }

  resetLeftFilters() {
    this.isSpot = null;
    this.isNew = null;
    this.isPTAapproved = null;
    this.isExchangeable = null;
    this.isDamage = null;
    this.selectedStorage = null;
    this.selectedRam = null;
    this.isProgress = false;
    this.getAllProducts();

  }

}
