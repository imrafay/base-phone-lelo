import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductAdvertServiceProxy, ProductAdvertViewDto, ProductAdvertDetailViewDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAddPostComponent } from '../create-or-edit-add-post/create-or-edit-add-post.component';
import { AbpSessionService } from 'abp-ng2-module';
import { Router, NavigationExtras } from '@angular/router';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})

export class UserDashboardComponent implements OnInit {
  @ViewChild('createoreditaddpost', { static: true }) createoreditaddpost: CreateOrEditAddPostComponent

  product: ProductAdvertViewDto[] = [];
  isInProgress: boolean =false;

  constructor(
    private _ProductAdvertService: ProductAdvertServiceProxy,
    private router: Router,
    private _sessionService: AbpSessionService

  ) { }

  ngOnInit(): void {
    this.getAllProducts(this._sessionService.userId)
  }
  getAllProducts(currentUserId:number) {
    this._ProductAdvertService.getAll(
      currentUserId,undefined,undefined,undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
    ).subscribe(res => {
      console.log(res)
      this.isInProgress=true;
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
