import { Component, OnInit, Injector, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { UserServiceProxy, DropdownOutputDto, CreateUserDto, RoleDto } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/session/app-session.service';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'app-create-user-location',
  templateUrl: './create-user-location.component.html',
  styleUrls: ['./create-user-location.component.css']
})
export class CreateUserLocationComponent extends AppComponentBase implements OnInit {

  CreateUserDto = new CreateUserDto();
  states: DropdownOutputDto[] = [];
  roles: RoleDto[] = [];
  city: DropdownOutputDto[] = [];
  neighbourhood: DropdownOutputDto[] = [];
  selectedState: DropdownOutputDto[] = [];
  selectedCity: DropdownOutputDto[] = [];
  selectedNeighbourhood: DropdownOutputDto[] = [];
  cities: SelectItem[];

  @Output() onSelectAllDropdownEvents: EventEmitter<any> = new EventEmitter<any>();


  constructor(private _UserServiceProxy: UserServiceProxy,
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private cd: ChangeDetectorRef
  ) {
    super(injector);

  }

  ngOnInit(): void {
    this.getAllStates()

  }

  getAllStates() {

    this._UserServiceProxy.getStates().subscribe(res => {
      this.states = res;
      this.cd.detectChanges();

    })
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
      this.onSelectDropdown()
      this.cd.detectChanges();
    })
  }
  onStateSelect(e) {
    if (this.selectedState) {
      this.getAllCities();
      this.onSelectDropdown()


    }
  }
  onSelectDropdown(): void {
    this.onSelectAllDropdownEvents.emit([this.selectedState, this.selectedCity, this.selectedNeighbourhood, this.neighbourhood.length]);
  }

  onCitySelect() {
    if (this.selectedCity) {
      this.getAllNeighbourhood();
      this.onSelectDropdown()

    }
    this.cd.detectChanges();
  }
  onSelectNeighbourhood() {
    this.onSelectDropdown()
  }

}
