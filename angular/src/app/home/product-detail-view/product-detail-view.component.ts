import { Component, OnInit } from '@angular/core';
import { ProductCompanyServiceProxy, DropdownOutputDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-product-detail-view',
  templateUrl: './product-detail-view.component.html',
  styleUrls: ['./product-detail-view.component.css']
})
export class ProductDetailViewComponent implements OnInit {
  productBrands: DropdownOutputDto[] = [];
  productBrandsLength;
  isShow = false;

  constructor(
    private _ProductCompanyService: ProductCompanyServiceProxy,

  ) { }

  ngOnInit(): void {
    this.getAllBrands()
  }

  getAllBrands() {
    this._ProductCompanyService.getProductCompanyDropdown().subscribe(res => {
      this.productBrands = res;
      this.productBrandsLength = res.length;
      if (this.productBrandsLength > 6) {
        setTimeout(() => {
          for (let index = 6; index < this.productBrandsLength + 1; index++) {
            document.getElementById('product-' + index).style.display = 'none';
          }
        }, 0);
      }
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
}
