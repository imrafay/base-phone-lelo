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
  commentUserEmail = '';
  commentUsername = '';
  commentText = '';
  commentsArray = [
    { id: 0, commentUsername: 'test1', commentUserEmail: 'test1@gmail.com', commentText: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Officiis, nemo ipsam eum illo minus voluptatibus ipsa nulla, perferendis aliquid aperiam beatae nihil sapiente eaque atque nesciunt perspiciatis ex saepe, quibusdam..' },
    { id: 0, commentUsername: 'test2', commentUserEmail: 'test2@gmail.com', commentText: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Officiis, nemo ipsam eum illo minus voluptatibus ipsa nulla, perferendis aliquid aperiam beatae nihil sapiente eaque atque nesciunt perspiciatis ex saepe, quibusdam..' },
    {
      id: 0, commentUsername: 'test3', commentUserEmail: 'test3@gmail.com', commentText: 'Lorem ipsum dolor sit amet, consectetur', commentReplies: [
        {
          id: 0, userName: 'test3@gmail.com', userReply: 'Lorem ipsum dolor sit amet, consectetur'
        }
      ]
    },
  ]
  myThumbnail = "https://wittlock.github.io/ngx-image-zoom/assets/thumb.jpg";
  myFullresImage = "https://wittlock.github.io/ngx-image-zoom/assets/fullres.jpg";
  replyInput = [];
  ngOnInit(): void {
    console.log(this.commentsArray)
    // this.getAllBrands();
    // this.getAllRams();
    // this.getAllStorages();
    this.getAllProducts();

  
    this.getProductById();
  }


  mouseEnter(id?, src?) {
    this.highlightedRows = [];
    this.imageShown = src?'https://phonelelostore.blob.core.windows.net/phonelelo/ProductImages/'+src.image:null;
    this.highlightedRows.push(id);
  }
  zoomScroll(s) {
    console.log(s)
  }

  getProductById() {
    this._ProductAdvertService.getProductAdvertForDetailView(parseInt(this.searchId)).subscribe(res => {
      this.isProgress = true;
      console.log(res)
      this.product = res;
      this.isProgress = true;
      this.images=res.images;
      this.imageShown ='https://phonelelostore.blob.core.windows.net/phonelelo/ProductImages/'+this.images[0].image;
      this.highlightedRows.push(this.images[0].id);
    })
  }
  getAllProducts() {
    this._ProductAdvertService.getRelatedAdsByAdvertId(
      parseInt(this.searchId),
      1,
      3
    ).subscribe(res => {
      console.log(res)
      this.products = res.items.slice(0,4);;
    })
  }
  onEnterReply(e, i) {
    this.replyInput = [];
    this.replyInput.push(i);
  }
}
