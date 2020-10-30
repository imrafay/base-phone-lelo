import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { ProductAdvertServiceProxy, ProductAdvertViewDto, ProductAdvertDetailViewDto, SiteStatisticsOutputDto, UserProfileReviewInputDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAddPostComponent } from '../create-or-edit-add-post/create-or-edit-add-post.component';
import { AbpSessionService } from 'abp-ng2-module';
import { Router, NavigationExtras } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})

export class UserDashboardComponent extends AppComponentBase  implements OnInit {
  @ViewChild('createoreditaddpost', { static: true }) createoreditaddpost: CreateOrEditAddPostComponent

  product: ProductAdvertViewDto[] = [];
  isInProgress: boolean =false;
  SiteStatisticsOutputDto:SiteStatisticsOutputDto=new SiteStatisticsOutputDto();
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
  replyInput = [];

  max = 5;
  rate = 4;
  isReadonly = false;
  UserProfileReviewInputDto: UserProfileReviewInputDto = new UserProfileReviewInputDto();

  constructor(
    private _ProductAdvertService: ProductAdvertServiceProxy,
    injector: Injector,
    private router: Router,
    private _sessionService: AbpSessionService

  ) {
    super(injector);
   }

  ngOnInit(): void {
    this.getAllProducts(this._sessionService.userId);
    this.getSiteStatistics();
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
    

    getSiteStatistics() {
      this._ProductAdvertService.getSiteStatistics().subscribe(res => {
        console.log(res)
        this.SiteStatisticsOutputDto=res;
      })
    }
    onSaveReview() {
      this.UserProfileReviewInputDto.review = this.commentText;
      this.UserProfileReviewInputDto.userId = this.appSession.userId;
  
    }
    onEnterReply(e, i) {
      this.replyInput = [];
      this.replyInput.push(i);
    }
    confirmSelection(event: KeyboardEvent) {
      if (event.keyCode === 13 || event.key === 'Enter') {
        this.isReadonly = true;
      }
    }
    
}
