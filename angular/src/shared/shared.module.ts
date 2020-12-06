import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppSessionService } from './session/app-session.service';
import { AppUrlService } from './nav/app-url.service';
import { AppAuthService } from './auth/app-auth.service';
import { AppRouteGuard } from './auth/auth-route-guard';
import { LocalizePipe } from '@shared/pipes/localize.pipe';

import { AbpPaginationControlsComponent } from './components/pagination/abp-pagination-controls.component';
import { AbpValidationSummaryComponent } from './components/validation/abp-validation.summary.component';
import { AbpModalHeaderComponent } from './components/modal/abp-modal-header.component';

import { AbpModalFooterComponent } from './components/modal/abp-modal-footer.component';
import { LayoutStoreService } from './layout/layout-store.service';

import { BusyDirective } from './directives/busy.directive';
import { EqualValidator } from './directives/equal-validator.directive';
import { CreateUserLocationComponent } from './components/create-user-location/create-user-location.component';
import { SignUpRegisterModalComponent } from './components/sign-up-register-modal/sign-up-register-modal.component';

import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';

import { AuthGuard } from './auth/auth-guard';
import { ProductCardComponent } from './components/product-card/product-card.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { NgOtpInputModule } from  'ng-otp-input';
import { PusherService } from './pusher-service.service';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        NgxPaginationModule,
        DropdownModule,
        ModalModule.forChild(),
        ProgressSpinnerModule,
        NgOtpInputModule
    ],
    declarations: [
        AbpPaginationControlsComponent,
        AbpValidationSummaryComponent,
        AbpModalHeaderComponent,
        AbpModalFooterComponent,
        LocalizePipe,
        BusyDirective,
        EqualValidator,
        CreateUserLocationComponent,
        ProductCardComponent,
        SignUpRegisterModalComponent,
    ],
    exports: [
        AbpPaginationControlsComponent,
        AbpValidationSummaryComponent,
        AbpModalHeaderComponent,
        AbpModalFooterComponent,
        LocalizePipe,
        BusyDirective,
        EqualValidator,
        CreateUserLocationComponent,
        SignUpRegisterModalComponent,
        ProductCardComponent,
    ],
    providers:[
        PusherService
    ]
})
export class SharedModule {
    static forRoot(): ModuleWithProviders<SharedModule> {
        return {
            ngModule: SharedModule,
            providers: [
                AppSessionService,
                AppUrlService,
                AppAuthService,
                AppRouteGuard,
                AuthGuard,
                LayoutStoreService
            ]
        };
    }
}
