import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { UserServiceProxy, CreateUserDto } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'signUpRegisterModal',
  templateUrl: './sign-up-register-modal.component.html',
  styleUrls: ['./sign-up-register-modal.component.css']
})
export class SignUpRegisterModalComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  CreateUserDto = new CreateUserDto()
  active = false;
  saving = false;
  mobileForm = true;
  verificationForm = false;
  signUpDetailForm = false;
  inputVerifyCode: string;
  inputNumber: string;
  inputEmail;
  inputPassword;
  userId:any;
  constructor(private _UserServiceProxy: UserServiceProxy,
    injector: Injector,
    private _AppSessionService: AppSessionService,
  ) {
    super(injector);

  }

  ngOnInit(): void {
  }
  show(productDetailId?: number): void {
    this.active = true;
    this.mobileForm = true;
    this.inputNumber = '';
    this.verificationForm = false;
    this.signUpDetailForm = false;
    this.modal.show();

  }

  signUpNumber(): void {
    this.mobileForm = false;
    this.verificationForm = true;
    this._UserServiceProxy.signUpUserByPhoneNumber(this.inputNumber, undefined).subscribe(res => {
      console.log(res)
      this.userId=res;
    })
  }

  validateNumber(event) {
    const keyCode = event.keyCode;

    const excludedKeys = [8, 37, 39, 46];

    if (!((keyCode >= 48 && keyCode <= 57) ||
      (keyCode >= 96 && keyCode <= 105) ||
      (excludedKeys.includes(keyCode)))) {
      event.preventDefault();
    }
  }
  // signUpDetail() {
  //   this.CreateUserDto.emailAddress = this.inputEmail;
  //   this.CreateUserDto.userName = this.inputEmail;
  //   this.CreateUserDto.name = this.inputEmail;
  //   this.CreateUserDto.password = this.inputPassword;
  //   this.CreateUserDto.surname = this.inputEmail;
  //   this._UserServiceProxy.create(this.CreateUserDto).subscribe(res => {
  //     setTimeout(() => {
  //       this.notify.success(this.l('SuccessfullyRegistered'));
  //       this.close()
  //     }, 1000);
  //   })
  //   console.log(this.CreateUserDto)
  // }
  // userVerified = false;
  sendVerificationCode() {
    let userId = this.userId;
    setTimeout(() => {
      this._UserServiceProxy.verifyUserPhoneNumberPost(userId, this.inputVerifyCode).subscribe(res => {
        console.log(res)
        if (res == false) {
          setTimeout(() => {
            this.notify.success(this.l('SuccessfullyRegistered'));
            this.close()
          }, 1000);

        }
      })
    }, 1000);


  }
  close(): void {
    this.active = false;
    this.modal.hide();
  }

}
