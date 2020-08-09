import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { UserServiceProxy, CreateUserDto } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppComponentBase } from '@shared/app-component-base';
import { LoginRegisterModalComponent } from '../login-register-modal/login-register-modal.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'signUpRegisterModal',
  templateUrl: './sign-up-register-modal.component.html',
  styleUrls: ['./sign-up-register-modal.component.css']
})
export class SignUpRegisterModalComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @ViewChild('loginRegisterModal', { static: true }) loginRegisterModal: LoginRegisterModalComponent;

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
  constructor(private _UserServiceProxy: UserServiceProxy,
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private _formBuilder: FormBuilder
  ) {
    super(injector);

  }
  isEditable = false;

  isLinear = false;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
  }
  show(): void {
    this.active = true;
    this.mobileFormBool = true;
    this.inputNumber = '';
    this.verificationFormBool = false;
    this.signUpDetailFormBool = false;
    this.modal.show();

  }

  signUpNumber(): void {
    // debugger
    this._UserServiceProxy.signUpUserByPhoneNumber(this.inputNumber, undefined).subscribe(res => {
      this.userId = res;
      console.log(res)
      this.mobileFormBool = false;
      this.verificationFormBool = true;
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
  inputIndividual;
  inputShopOwner;
  registerAsUserChoice = false;
  sendVerificationCode() {

    this._UserServiceProxy.verifyUserPhoneNumberPost(this.userId, this.inputVerifyCode).subscribe(res => {
      console.log(res)
      if (res == true) {
        // debugger
        this.registerAsUserChoice = true
        // document.getElementById('signUpDetail').style.display='block'
        this.mobileFormBool = false;
        this.verificationFormBool = false;
        // setTimeout(() => {
        //   // this.notify.success(this.l('SuccessfullyRegistered'));
        //   this.close()
        //   this.loginRegisterModal.show()zz
        // }, 1000);

      }
      else {
        // document.getElementById('signUpDetail').style.display='none'
      }

    })




  }
  close(): void {
    this.active = false;
    this.modal.hide();
  }

}
