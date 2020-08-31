import { Component, OnInit } from '@angular/core';
import { ProductAdvertServiceProxy, ProductAdvertViewDto } from '@shared/service-proxies/service-proxies';
import { AbpSessionService } from 'abp-ng2-module';

@Component({
  selector: 'app-public-home',
  templateUrl: './public-home.component.html',
  styleUrls: ['./public-home.component.css'],
})
export class PublicHomeComponent implements OnInit {
  isInProgress:boolean=false;
  products: ProductAdvertViewDto[] = [];
userName:string;
  constructor(
    private _ProductAdvertService: ProductAdvertServiceProxy,
    private _sessionService: AbpSessionService


  ) { }

  ngOnInit(): void {
    this.getAllProducts()
    console.log(this._sessionService)
  }
  getAllProducts() {
    this._ProductAdvertService.getAll(
      undefined,undefined,undefined,undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
    ).subscribe(res => {
      console.log(res)
      this.isInProgress=true;
      this.products = res.items;
    })
  }
}
