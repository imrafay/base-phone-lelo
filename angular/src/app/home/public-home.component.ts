import { Component, OnInit } from '@angular/core';
import { ProductAdvertServiceProxy, ProductAdvertViewDto } from '@shared/service-proxies/service-proxies';
import { AbpSessionService } from 'abp-ng2-module';




@Component({
  selector: 'app-public-home',
  templateUrl: './public-home.component.html',
  styleUrls: ['./public-home.component.css'],
})
export class PublicHomeComponent implements OnInit {

  isInProgress: boolean = false;
  products: ProductAdvertViewDto[] = [];
  usedProducts: ProductAdvertViewDto[] = [];
  newProducts: ProductAdvertViewDto[] = [];
  userName: string;
  itemsPerSlide = 4;
  singleSlideOffset = true;
  slides = [];
  searchFilter = ''

  constructor(
    private _ProductAdvertService: ProductAdvertServiceProxy,
    private _sessionService: AbpSessionService,

  ) {
  }

  ngOnInit(): void {
    this.getAllProducts();
    this.getAllUsedProducts();
    this.getAllDamagedProducts();
    console.log(this._sessionService)

  }
  getAllProducts() {
    this._ProductAdvertService.getAll(
      undefined, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
    ).subscribe(res => {
      console.log(res)
      this.isInProgress = true;
      this.products = res.items;
      console.log(this.slides)
    })
  }
  getAllUsedProducts() {
    this._ProductAdvertService.getAll(
      undefined, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, false, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
    ).subscribe(res => {
      console.log(res)
      this.isInProgress = true;
      this.usedProducts = res.items;

    })
  }

  getAllDamagedProducts() {
    this._ProductAdvertService.getAll(
      undefined, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, true, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
    ).subscribe(res => {
      console.log(res)
      this.isInProgress = true;
      this.newProducts = res.items;
    })
  }
}
