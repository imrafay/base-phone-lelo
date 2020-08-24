import { Component, OnInit, Injector } from '@angular/core';
import { AppSessionService } from '@shared/session/app-session.service';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { AppComponentBase } from '@shared/app-component-base';
import { DropdownOutputDto, RoleDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppConsts } from '@shared/AppConsts';
import { HttpErrorResponse } from '@angular/common/http';

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
  uploadUrl: string;
  baseUrl = AppConsts.remoteServiceBaseUrl;
  fileInQue: boolean = false;
  uploadedFiles: any[] = [];

  constructor(
    injector: Injector,
    private _AppSessionService: AppSessionService,
    private _authService: AppAuthService,
    private _UserServiceProxy: UserServiceProxy,

  ) {
    super(injector);
     this.uploadUrl = this.baseUrl + "api/File/UploadFiles";


    console.log(this.appSession)

  }
  ngOnInit(): void {
    this.getAllCities()
    this.getAllStates()
  }

  onSelectDropdown(valuesArray): void {
    console.log(valuesArray)
    if (valuesArray) {
      // this.selectedState = valuesArray[0];
      // this.selectedCity = valuesArray[1];
      // this.selectedNeighbourhood = valuesArray[2];
      // this.neighbourhood = valuesArray[3];
    }

    // this.isValid(valuesArray)
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

  
    onUpload(event): void {
        console.log(event);
        if (event.files.length > 0) {
            const originalEvent = event.originalEvent;
            if (originalEvent.body.success) {
              for(let file of event.files) {
                this.uploadedFiles.push(file);
            }
                   console.log(originalEvent.body.result[0].fileName);
                this.notify.success(this.l("File Uploaded"));
         
            } else {
                    
                    console.log(originalEvent.body.error.message);
            
            }
        }
    }
    onSelect(event) {
      this.fileInQue = true;
     
  }
  onRemove(event) {
    this.fileInQue = false;
  
}
  onBeforeUpload(event): void {
    console.log("event", event);
    event.formData.append("PhoneLeloDataFileType", 1);
  }
  
  onError(event) {
    console.log(event);
    let error: HttpErrorResponse = event.error;
    if (error) {
        console.log(error)
      }
}
  logout(): void {
    this._authService.logout();
  }

}
