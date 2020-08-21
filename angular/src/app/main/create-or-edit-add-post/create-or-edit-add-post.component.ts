import { Component, OnInit, Injector } from '@angular/core';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { AppComponentBase } from '@shared/app-component-base';
import { DropdownOutputDto, RoleDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-create-or-edit-add-post',
  templateUrl: './create-or-edit-add-post.component.html',
  styleUrls: ['./create-or-edit-add-post.component.css']
})
export class CreateOrEditAddPostComponent  extends AppComponentBase implements OnInit {

  states: DropdownOutputDto[] = [];
  roles: RoleDto[] = [];
  city: DropdownOutputDto[] = [];
  neighbourhood: DropdownOutputDto[] = [];
  selectedState: DropdownOutputDto[] = [];

  constructor(
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private _authService: AppAuthService,
    private _UserServiceProxy: UserServiceProxy,

  ) {
    super(injector);
    console.log(this.appSession)

  }
  ngOnInit(): void {
    this.getAllCities()
    this.getAllStates()
  }
  getAllStates() {

    this._UserServiceProxy.getStates().subscribe(res => {
      this.states = res;
      console.log(this.states)

    })
    console.log(this.city)
  }
  getAllCities() {

    this._UserServiceProxy.getCitiesByStateId(4).subscribe(res => {
      this.city = res;

    })
  }
  logout(): void {
    this._authService.logout();
  }

}
