import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductAdvertServiceProxy, ProductAdvertViewDto, ProductAdvertDetailViewDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAddPostComponent } from '../create-or-edit-add-post/create-or-edit-add-post.component';
import { Router, NavigationExtras } from '@angular/router';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})

export class UserDashboardComponent implements OnInit {
  @ViewChild('createoreditaddpost', { static: true }) createoreditaddpost: CreateOrEditAddPostComponent

  product: ProductAdvertViewDto[] = [];

  constructor(
    private _ProductAdvertService: ProductAdvertServiceProxy,
    private router: Router

  ) { }

  ngOnInit(): void {
    this.getAllProducts()
  }
  getAllProducts() {
    this._ProductAdvertService.getAll(
      undefined,undefined,undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
    ).subscribe(res => {
      console.log(res)
      this.product = res.items;
    })
  }
  getProductById(productId: number) {
    let objToSend: NavigationExtras = {
      queryParams: {
    id:productId
    }
  }
    this.router.navigate(['/app/main/add-post'], { 
      state: { productdetails: objToSend }
    });  }
}
