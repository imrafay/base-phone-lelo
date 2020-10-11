import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductCompanyServiceProxy, DropdownOutputDto, ProductAdvertServiceProxy, ProductAdvertViewDto, ProductAdvertDetailViewDto } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute } from '@angular/router';
import { ViewportScroller } from '@angular/common';

@Component({
  selector: 'product-detail-view.component',
  templateUrl: './product-detail-view.component.html',
  styleUrls: ['./product-detail-view.component.css']
})
export class ProductDetailViewComponent implements OnInit {
  productBrands: DropdownOutputDto[] = [];
  productBrandsLength;
  isShow = false;
  highlightedRows = []
  product: ProductAdvertDetailViewDto = new ProductAdvertDetailViewDto();
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
  searchId = '';
  isDamage;
  isNegotiable;
  ramSelected = [];
  storageSelected = [];

  constructor(
    private _ProductCompanyService: ProductCompanyServiceProxy,
    private _ProductAdvertService: ProductAdvertServiceProxy,
    private _route: ActivatedRoute,
    private viewportScroller: ViewportScroller


  ) {
    console.log(this._route.params)
    this.searchId = this._route.params ? this._route.params['value'].id : ''
  }
  images = [];
  imageShown;
  @ViewChild("aboutus", { static: false }) aboutus;
  @ViewChild("contact", { static: false }) contact;
  products: ProductAdvertViewDto[] = [];


  ngOnInit(): void {
    // this.getAllBrands();
    // this.getAllRams();
    // this.getAllStorages();
    this.getAllProducts();

    this.images = [
      { src: 'assets/img/phone-img/phone1.jpg', id: 0 },
      { src: 'assets/img/phone-img/phone2.jpg', id: 1 },
      { src: 'assets/img/phone-img/phone3.jpg', id: 2 },
      { src: 'assets/img/phone-img/phone1.jpg', id: 3 },
      { src: 'assets/img/phone-img/phone2.jpg', id: 4 },
    ]
    this.imageShown = this.images[0].src;
    this.getProductById();
    this.highlightedRows.push(this.images[0].id)
  }
  myThumbnail="https://wittlock.github.io/ngx-image-zoom/assets/thumb.jpg";
  myFullresImage="https://wittlock.github.io/ngx-image-zoom/assets/fullres.jpg";

  mouseEnter(id?, src?) {
    this.highlightedRows = [];
    this.imageShown = src;
    this.highlightedRows.push(id);
  }
  zoomScroll(s){
    console.log(s)
  }

  getProductById() {
    this._ProductAdvertService.getProductAdvertForDetailView(parseInt(this.searchId)).subscribe(res => {
      this.isProgress = true;
      console.log(res)
      // this.isInProgress = true;
      this.product = res;
    })
  }
  getAllProducts() {
    this._ProductAdvertService.getRelatedAdsByAdvertId(
      parseInt(this.searchId),
      1,
      3
    ).subscribe(res => {
      console.log(res)
      this.products = res.items;
    })
  }

}
