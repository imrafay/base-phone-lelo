import {
  Component, OnInit, ViewChild, Injector, ChangeDetectionStrategy,
  ChangeDetectorRef,
  ElementRef
} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { UserServiceProxy, CreateUserDto, DropdownOutputDto, RoleDto } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppComponentBase } from '@shared/app-component-base';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'signUpRegisterModal',
  templateUrl: './sign-up-register-modal.component.html',
  styleUrls: ['./sign-up-register-modal.component.css'],
  // changeDetection: ChangeDetectionStrategy.OnPush

})
export class SignUpRegisterModalComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @ViewChild('someElement', { static: false }) someElement: ElementRef;

  CreateUserDto = new CreateUserDto();
  states: DropdownOutputDto[]=[];
  roles:RoleDto[]=[];
  city: DropdownOutputDto[]=[];
  neighbourhood: DropdownOutputDto[]=[];
  selectedState: DropdownOutputDto[]=[];
  selectedCity: DropdownOutputDto[]=[];
  selectedNeighbourhood: DropdownOutputDto[]=[];
  active = false;
  saving = false;
  mobileFormBool = true;
  verificationFormBool = false;
  locationDetailForm = false;
  signUpDetailFormBool = false;
  inputVerifyCode: string;
  incorrectCode = false;
  inputNumber: string;
  inputEmail;
  inputPassword;
  userId: any;
  inputRegisterOption
  registerAsUserChoice = false;
  inputName
  inputSurName
  constructor(private _UserServiceProxy: UserServiceProxy,
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private cd: ChangeDetectorRef
  ) {
    super(injector);

  }

  ngOnInit() {
    this.getRoles()

    this.getAllStates();
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
    // this.mobileFormBool = false;
    this.registerAsUserChoice = false;
    // this.verificationFormBool = false;
    // this.locationDetailForm = false;
  }

  changeRole = (e) => this.inputRegisterOption = e.target.value;

  signUpNumber(): void {
    // debugger
    this._UserServiceProxy.signUpUserByPhoneNumber(this.inputNumber, undefined).subscribe(res => {
      this.userId = res;
      this.mobileFormBool = false;
      this.verificationFormBool = true;
      this.cd.detectChanges();
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
    this.CreateUserDto.name = this.inputName;
    // this.CreateUserDto.password = this.inputPassword;
    this.CreateUserDto.surname = this.inputSurName;
    // this._UserServiceProxy.completeUserProfile(this.CreateUserDto.userName,
    //   this.CreateUserDto.name, this.CreateUserDto.surname, this.CreateUserDto.emailAddress,
    //   true, undefined, undefined, undefined,
    //   undefined, null).subscribe(res => {
    //     this.notify.success(this.l('SuccessfullyRegistered'));
    //     this.cd.detectChanges();
    //   })
    this.signUpDetailFormBool = false;
    this.locationDetailForm = true;
    console.log(this.CreateUserDto)
  }

  sendVerificationCode() {
    this._UserServiceProxy.verifyUserPhoneNumberPost(this.userId, this.inputVerifyCode).subscribe(res => {
      console.log(res)
      if (res == true) {
        this.registerAsUserChoice = true
        this.mobileFormBool = false;
        this.verificationFormBool = false;
        this.cd.detectChanges();

      }
      else {
        this.incorrectCode = true;
        this.cd.detectChanges();

      }
    })

  }

  getRoles() {

    this._UserServiceProxy.getRoles().subscribe(res => {
      this.roles = res.items;
      console.log(this.roles)
      this.cd.detectChanges();

    })
  }

  getAllStates() {

    this._UserServiceProxy.getStates().subscribe(res => {
      this.states = res;
      console.log(this.states)
      this.cd.detectChanges();

    })
    console.log(this.city)
  }
  getAllCities() {

    this._UserServiceProxy.getCitiesByStateId(this.selectedState['id']).subscribe(res => {
      this.city = res;
      this.cd.detectChanges();

    })
  }

  getAllNeighbourhood() {
    this._UserServiceProxy.getNeighbourhoodsByCityId(this.selectedCity['id']).subscribe(res => {
      this.neighbourhood = res;
      this.cd.detectChanges();
    })
  }
  onStateSelect() {
    if (this.selectedState) {
      this.getAllCities();

    }
  }

  onCitySelect() {
    if (this.selectedCity) {
      this.getAllNeighbourhood();

    }
    this.cd.detectChanges();
  }
  isValid() {
    return (
      this.selectedNeighbourhood &&
      this.selectedCity &&
      this.selectedState
    )
  }
  saveLocation() {
    console.log(this.selectedNeighbourhood,
      this.selectedCity,
      this.selectedState)
  }

  close(): void {
    this.active = false;
    this.modal.hide();
  }

}
