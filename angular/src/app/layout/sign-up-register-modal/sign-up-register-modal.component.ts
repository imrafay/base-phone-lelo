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
  mobileFormBool = true;
  verificationFormBool = false;
  signUpDetailFormBool = false;
  inputVerifyCode: string;
  inputNumber: string;
  inputEmail;
  inputPassword;
  userId: any;
  inputRegisterOption
  registerAsUserChoice = false;
  constructor(private _UserServiceProxy: UserServiceProxy,
    injector: Injector,
    private _AppSessionService: AppSessionService,
  ) {
    super(injector);

  }

  ngOnInit() {

  }
  show(): void {
    this.active = true;
    this.mobileFormBool = true;
    this.inputNumber = '';
    this.verificationFormBool = false;
    this.signUpDetailFormBool = false;
    this.modal.show();

  }
  registerOption() {
    this.signUpDetailFormBool = true;
    this.mobileFormBool = false;
    this.registerAsUserChoice = false;
    this.verificationFormBool = false;
  }

  changeOption = (e) => this.inputRegisterOption = e.target.value;

  signUpNumber(): void {
    // debugger
    this.mobileFormBool = false;
    this.verificationFormBool = true;
    this._UserServiceProxy.signUpUserByPhoneNumber(this.inputNumber, undefined).subscribe(res => {
      this.userId = res;
      console.log(res)
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
  signUpDetail() {
    this.CreateUserDto.emailAddress = this.inputEmail;
    this.CreateUserDto.userName = this.inputEmail;
    this.CreateUserDto.name = this.inputEmail;
    this.CreateUserDto.password = this.inputPassword;
    this.CreateUserDto.surname = this.inputEmail;
    this._UserServiceProxy.completeUserProfile(this.CreateUserDto.userName,
      this.CreateUserDto.emailAddress, this.CreateUserDto.emailAddress, this.CreateUserDto.emailAddress,
      true, this.CreateUserDto.emailAddress, undefined, undefined,
      undefined, undefined).subscribe(res => {
        setTimeout(() => {
          this.notify.success(this.l('SuccessfullyRegistered'));
          this.close()
        }, 1000);
      })
    console.log(this.CreateUserDto)
  }

  sendVerificationCode() {
    this._UserServiceProxy.verifyUserPhoneNumberPost(this.userId, this.inputVerifyCode).subscribe(res => {
      console.log(res)

    })
    this.registerAsUserChoice = true
    this.mobileFormBool = false;
    this.verificationFormBool = false;
  }
  close(): void {
    this.active = false;
    this.modal.hide();
  }

}
