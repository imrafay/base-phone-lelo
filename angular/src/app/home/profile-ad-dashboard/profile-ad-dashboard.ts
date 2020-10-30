import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { ProductAdvertServiceProxy, ProductAdvertViewDto, ProductAdvertDetailViewDto, SiteStatisticsOutputDto, UserProfileReviewInputDto, UserProfileReviewServiceProxy, UserProfileReviewOutputDto } from '@shared/service-proxies/service-proxies';
// import { CreateOrEditAddPostComponent } from '../create-or-edit-add-post/create-or-edit-add-post.component';
import { AbpSessionService } from 'abp-ng2-module';
import { Router, NavigationExtras, ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { CreateOrEditAddPostComponent } from '@app/main/create-or-edit-add-post/create-or-edit-add-post.component';
import { SignUpRegisterModalComponent } from '@shared/components/sign-up-register-modal/sign-up-register-modal.component';


@Component({
  selector: 'profile-ad-dashboard',
  templateUrl: './profile-ad-dashboard.html',
  styleUrls: ['./profile-ad-dashboard.css']
})

export class ProfileAdDashboard extends AppComponentBase implements OnInit {
  @ViewChild('createoreditaddpost', { static: true }) createoreditaddpost: CreateOrEditAddPostComponent
  product: ProductAdvertViewDto[] = [];
  isInProgress: boolean = false;
  SiteStatisticsOutputDto: SiteStatisticsOutputDto = new SiteStatisticsOutputDto();
  commentUserEmail = '';
  commentUsername = '';
  commentText = '';
  commentsArray = [
    { id: 0, commentUsername: 'test1', commentUserEmail: 'test1@gmail.com', commentText: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Officiis, nemo ipsam eum illo minus voluptatibus ipsa nulla, perferendis aliquid aperiam beatae nihil sapiente eaque atque nesciunt perspiciatis ex saepe, quibusdam..' },
    { id: 0, commentUsername: 'test2', commentUserEmail: 'test2@gmail.com', commentText: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Officiis, nemo ipsam eum illo minus voluptatibus ipsa nulla, perferendis aliquid aperiam beatae nihil sapiente eaque atque nesciunt perspiciatis ex saepe, quibusdam..' },
  ]
  replyInput = [];

  max = 5;
  rate = 5;
  isReadonly = false;
  UserProfileReviewInputDto: UserProfileReviewInputDto = new UserProfileReviewInputDto();
  searchId: number;
  @ViewChild('signUpRegisterModal', { static: true }) signUpRegisterModal: SignUpRegisterModalComponent;
  isReviewEnable = false;
  review: UserProfileReviewOutputDto = new UserProfileReviewOutputDto();
  constructor(
    private _ProductAdvertService: ProductAdvertServiceProxy,
    private _userProfileReviewServiceProxy: UserProfileReviewServiceProxy,
    injector: Injector,
    private router: Router,
    private _sessionService: AbpSessionService,
    private _route: ActivatedRoute,

  ) {
    super(injector);
    this.searchId = this._route.params ? this._route.params['value'].id : ''

  }

  ngOnInit(): void {
    this.appSession && this.appSession.userId ? this.isReviewEnable = true : null;
    this.getAllProducts();
    this.getSiteStatistics();
    this.getAllReviews();
  }
  onSelectRating(e) {
    console.log(e)
    e && e.value ? this.rate = e.value : 0;
  }
  getAllProducts() {
    this._ProductAdvertService.getAll(
      this.searchId, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
      undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
    ).subscribe(res => {
      console.log(res)
      this.isInProgress = true;
      this.product = res.items;
    })
  }
  getProductById(productId: number) {
    let objToSend: NavigationExtras = {
      queryParams: {
        id: productId
      }
    }
    this.router.navigate(['/app/main/add-post'], {
      state: { productdetails: objToSend }
    });
  }


  getSiteStatistics() {
    this._ProductAdvertService.getSiteStatistics().subscribe(res => {
      console.log(res)
      this.SiteStatisticsOutputDto = res;
    })
  }
  onClickReview() {
    if (this.appSession.user) {
      this.isReviewEnable = true
    }
    else {
      this.signUpRegisterModal.show();
    }
  }
  onSaveReview() {
    this.UserProfileReviewInputDto.review = this.commentText;
    this.UserProfileReviewInputDto.userId = this.searchId;
    this.UserProfileReviewInputDto.reviewerId = this.appSession.userId;
    this.UserProfileReviewInputDto.rating = this.rate;
    console.log(this.rate)
    this._userProfileReviewServiceProxy.create(this.UserProfileReviewInputDto).subscribe(res => {
      console.log(res)
      this.notify.success(this.l('Review send'));
      this.getAllReviews();
      this.reset();
    })
  }
  getAllReviews() {
    this._userProfileReviewServiceProxy.getUserReviews(0, this.searchId).subscribe(res => {
      console.log(res)
      this.review = res;
      res.userProfileReviewOutputList.map(res => res['firstLetter'] = res.reviewerFullName.substring(0, 1))
    })
  }
  reset() {
    this.commentText = '';
    this.rate = 5;
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

  generateAvater(nameString: string, i: number, idTag: string) {
    var colours = ["#1abc9c", "#2ecc71", "#3498db", "#9b59b6", "#34495e", "#16a085", "#27ae60", "#2980b9", "#8e44ad", "#2c3e50", "#f1c40f", "#e67e22", "#e74c3c", "#ecf0cc", "#95a5a6", "#f39c12", "#d35400", "#c0392b", "#bdc3c7", "#7f8c8d"];
    var name = nameString,
      nameSplit = name.split(" "),
      // initials = nameSplit[0].charAt(0).toUpperCase() + nameSplit[1].charAt(0).toUpperCase();
      initials = nameSplit[0].charAt(0).toUpperCase();

    var recipientName = document.getElementById(idTag + i)
    recipientName.innerText = initials;

    var charIndex = initials.charCodeAt(0) - 64,
      colourIndex = charIndex % 20;
    colourIndex < 1 ? colourIndex = 1 : null;
    recipientName.style.background = colours[colourIndex - 1];
    recipientName.style.textAlign = "center";
    recipientName.style.color = "white";
  }
}
