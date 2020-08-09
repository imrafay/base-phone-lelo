import { Component, ChangeDetectionStrategy, ViewChild } from '@angular/core';
import { SignUpRegisterModalComponent } from './sign-up-register-modal/sign-up-register-modal.component';

@Component({
  selector: 'app-public-header',
  templateUrl: './public-header.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PublicHeaderComponent {
  @ViewChild('signUpRegisterModal', { static: true }) signUpRegisterModal: SignUpRegisterModalComponent;

}
