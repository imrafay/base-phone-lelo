import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AppAuthService } from '@shared/auth/app-auth.service';

@Component({
  selector: 'app-user-siderbar',
  templateUrl: './user-siderbar.component.html',
  styleUrls: ['./user-siderbar.component.css']
})
export class UserSiderbarComponent extends AppComponentBase  implements OnInit {

  constructor(
    injector: Injector,
    private _authService: AppAuthService,

  ) { 
    super(injector);

  }

  logout(): void {
    this._authService.logout();
  }
  ngOnInit(): void {
  }

}
