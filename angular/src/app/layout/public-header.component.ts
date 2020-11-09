import { Component, ChangeDetectionStrategy, ViewChild, Injector, OnInit, Output, EventEmitter } from '@angular/core';
import { SignUpRegisterModalComponent } from '@shared/components/sign-up-register-modal/sign-up-register-modal.component';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppComponentBase } from '@shared/app-component-base';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-public-header',
  templateUrl: './public-header.component.html',
  styleUrls:['./public-header.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PublicHeaderComponent extends AppComponentBase implements OnInit {
  @ViewChild('signUpRegisterModal', { static: true }) signUpRegisterModal: SignUpRegisterModalComponent;
  isUserValid: Boolean = false;
  highlightedRows = [];
  searchFilter='';

  constructor(
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private _authService: AppAuthService,
    private _router: Router,
    private _route: ActivatedRoute,


  ) {
    super(injector);
    this._router.url == '/app/home' ? this.highlightedRows.push(1) : null;
    this._router.url == '/app/main/user-dashboard' ? this.highlightedRows.push(2) : null;
    this._router.url == '/app/main/add-post' ? this.highlightedRows.push(3) : null;
    console.log(this.appSession)
    console.log(this._router.url)
    this.searchFilter = this._router.routerState.snapshot.url.split('/')[4];
    console.log(this._router)

  }
  ngOnInit() {

    if (this.appSession.user) {
      this.isUserValid = true
    }
    else {
      this.isUserValid = false
    }
    console.log(this.appSession)
    console.log(this._route)

  }
  logout(): void {
    this._authService.logout();
  }
  onSelectMenu(id) {

    this.highlightedRows.length > 0 ? this.highlightedRows = [] : null;
    this.highlightedRows.push(id);

  }
  signUpRegisterModalShow() {
    if (this.appSession.user) {
      this._router.navigate(['/app/main/add-post']);
    }
    else {
      this.signUpRegisterModal.show();
    }
  }

}
