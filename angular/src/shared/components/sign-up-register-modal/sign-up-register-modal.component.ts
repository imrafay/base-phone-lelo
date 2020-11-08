import {
  Component, OnInit, ViewChild, Injector, ChangeDetectionStrategy,
  ChangeDetectorRef,
  ElementRef,
  EventEmitter,
  Output
} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { UserServiceProxy, CreateUserDto, DropdownOutputDto, RoleDto } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppComponentBase } from '@shared/app-component-base';
import { CreateUserLocationComponent } from '@shared/components/create-user-location/create-user-location.component';
import { AppAuthService } from '@shared/auth/app-auth.service';

@Component({
  selector: 'signUpRegisterModal',
  templateUrl: './sign-up-register-modal.component.html',
  styleUrls: ['./sign-up-register-modal.component.css'],
  // changeDetection: ChangeDetectionStrategy.OnPush

})
export class SignUpRegisterModalComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @ViewChild('appcreateuserlocation', { static: true }) appcreateuserlocation: CreateUserLocationComponent;

  CreateUserDto = new CreateUserDto();
  roles: RoleDto[] = [];

  neighbourhood: number = 0;
  selectedState: string = ''
  selectedCity: string = ''
  selectedNeighbourhood: string = ''
  active: Boolean = false;
  saving: Boolean = false;
  mobileFormBool: Boolean = true;
  verificationFormBool: Boolean = false;
  locationDetailForm: Boolean = false;
  signUpDetailFormBool: Boolean = false;
  inputVerifyCode: string;
  incorrectCode: Boolean = false;
  inputNumber: string;
  inputEmail: string = '';
  inputPassword: string = '';
  userId: any;
  inputRegisterOption
  registerAsUserChoice: Boolean = false;
  inputName: string = '';
  inputSurName: string = '';
  phoneCode = '03';
  isSignUpNumberSpinner = false;
  isVerifyCodeSpinner = false;
  isSignUpDetailsSpinner = false;
  @Output() newItemEvent = new EventEmitter<string>();


  constructor(private _UserServiceProxy: UserServiceProxy,
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private cd: ChangeDetectorRef,
    public authService: AppAuthService,

  ) {
    super(injector);
    console.log(this.appSession)

  }

  ngOnInit() {
    this.getRoles()

    // this.getAllStates();
  }
  show(): void {
    this.active = true;
    this.mobileFormBool = true;
    this.inputNumber = '';
    this.verificationFormBool = false;
    this.locationDetailForm = false;
    this.signUpDetailFormBool = false;
    this.modal.show();

  }
  registerOption() {
    console.log(this.inputRegisterOption)
    this.signUpDetailFormBool = true;
    this.registerAsUserChoice = false;
  }

  changeRole = (e) => this.inputRegisterOption = e.target.value;

  signUpNumber(): void {
    this.isSignUpNumberSpinner = true;
    this._UserServiceProxy.signUpUserByPhoneNumber(this.phoneCode + this.inputNumber, undefined).subscribe(res => {
      this.userId = res;
      this.mobileFormBool = false;
      this.verificationFormBool = true;
      this.cd.detectChanges();
      this.isSignUpNumberSpinner = false
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
  onSelectDropdown(valuesArray): void {
    console.log(valuesArray)
    if (valuesArray) {
      this.selectedState = valuesArray[0];
      this.selectedCity = valuesArray[1];
      this.selectedNeighbourhood = valuesArray[2];
      this.neighbourhood = valuesArray[3];
    }
  }
  
  signUpDetail() {
    this.isSignUpDetailsSpinner = true;
    this.CreateUserDto.emailAddress = this.inputEmail;
    this.CreateUserDto.userName = this.inputEmail;
    this.CreateUserDto.name = this.inputName;
    // this.CreateUserDto.password = this.inputPassword;
    this.CreateUserDto.surname = this.inputSurName;
    this._UserServiceProxy.completeUserProfile(this.CreateUserDto.userName,
      this.CreateUserDto.name, this.CreateUserDto.surname, this.CreateUserDto.emailAddress,
      true, undefined, undefined, undefined,
      undefined, this.userId).subscribe(res => {
        console.log(res)
        this.locationDetailForm = true;
        this.signUpDetailFormBool = false;
        this.notify.success(this.l('SuccessfullyRegistered'));
        this.isSignUpDetailsSpinner = false;
        this.cd.detectChanges();
      })
    this.isSignUpDetailsSpinner = false;
    this.cd.detectChanges();
    console.log(this.CreateUserDto)
  }

  sendVerificationCode() {
    this.isVerifyCodeSpinner = true;
    this._UserServiceProxy.verifyUserPhoneNumber(this.userId, this.inputVerifyCode).subscribe(res => {
      console.log(res)
      if (res == true) {
        this.registerAsUserChoice = true
        this.mobileFormBool = false;
        this.verificationFormBool = false;
        this.cd.detectChanges();
        this.isVerifyCodeSpinner = false;
      }
      else {
        this.incorrectCode = true;
        this.cd.detectChanges();
        this.isVerifyCodeSpinner = false;
        this.cd.detectChanges();
      }
    })
  }

  getRoles() {
    this._UserServiceProxy.getRegistrationRoles().subscribe(res => {
      this.roles = res.items;
      console.log(this.roles)
      this.cd.detectChanges();

    })
  }


  isValid() {
    // console.log(valuesArray)
    console.log(this.neighbourhood)
    if (this.selectedCity &&
      this.selectedState && this.selectedNeighbourhood.length == 0 && this.neighbourhood == 0)
      return (
        this.selectedCity['id'] &&
        this.selectedState['id']
      )
    if (this.selectedCity &&
      this.selectedState && this.selectedNeighbourhood)
      return (
        this.selectedCity['id'] &&
        this.selectedState['id'] &&
        this.selectedNeighbourhood['id']
      )

  }

  saveLocation() {
    console.log(this.selectedNeighbourhood,
      this.selectedCity,
      this.selectedState)
    this._UserServiceProxy.updateUserLocation(this.userId, this.selectedState['id'], this.selectedCity['id'],
      this.selectedNeighbourhood['id'] ? this.selectedNeighbourhood['id'] : null).subscribe(res => {
        console.log(res)
        this.notify.success(('Location Updated'));
        this.authService.authenticateModel.password = this.inputVerifyCode
        this.authService.authenticateModel.userNameOrEmailAddress = this.inputEmail
        this.authService.authenticateModel.rememberClient = true;
        this.authService.authenticate(() => console.log('res'));
        // setTimeout(() => this.close(), 2000);
        this.cd.detectChanges();
      })

  }


  close(): void {
    this.active = false;
    this.modal.hide();
  }

}
