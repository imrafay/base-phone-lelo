import { Component, OnInit } from '@angular/core';
import { ProductCompanyServiceProxy, DropdownOutputDto, ProductAdvertServiceProxy, ProductAdvertViewDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-product-detail-view',
  templateUrl: './product-detail-view.component.html',
  styleUrls: ['./product-detail-view.component.css']
})
export class ProductDetailViewComponent implements OnInit {
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
  isSpot: DropdownOutputDto[] = [];
  
  constructor(
    private _ProductCompanyService: ProductCompanyServiceProxy,
    private _ProductAdvertService: ProductAdvertServiceProxy,


  ) { }

  ngOnInit(): void {
    this.getAllBrands();
    this.getAllProducts();
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
  getAllProducts() {
    this._ProductAdvertService.getAll(
      undefined, undefined, undefined, undefined, undefined,
      this.productCompanyId, undefined, undefined, undefined, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
    ).subscribe(res => {
      this.isProgress = true;
      console.log(res)
      // this.isInProgress = true;
      this.products = res.items;
      this.productsLength = res.items.length;

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
  resetFilters() {
    this.isProgress = false;
    this.productCompanyId = null;
    this.highlightedRows = [];
    this.products = [];
    this.getAllProducts();
  }

}
