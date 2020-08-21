import { Component, ChangeDetectionStrategy, ViewChild, Injector, OnInit } from '@angular/core';
import { SignUpRegisterModalComponent } from './sign-up-register-modal/sign-up-register-modal.component';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppComponentBase } from '@shared/app-component-base';
import { AppAuthService } from '@shared/auth/app-auth.service';

@Component({
  selector: 'app-public-header',
  templateUrl: './public-header.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PublicHeaderComponent extends AppComponentBase implements OnInit {
  @ViewChild('signUpRegisterModal', { static: true }) signUpRegisterModal: SignUpRegisterModalComponent;
  isUserValid: Boolean=false;
  constructor(
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private _authService: AppAuthService

  ) {
    super(injector);
    console.log(this.appSession)

  }
  ngOnInit() {
    
    if (this.appSession.user) {
      this.isUserValid = true
    }
    else {
      this.isUserValid = false
    }
    console.log(this.appSession)

  }
  logout(): void {
    this._authService.logout();
  }
}
