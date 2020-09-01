// import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AppSessionService } from '@shared/session/app-session.service';
import { PermissionCheckerService } from 'abp-ng2-module';
// import { AppSessionService } from '@shared/common/session/app-session.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(
        private _permissionChecker: PermissionCheckerService,
        private _router: Router,
        private _sessionService: AppSessionService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (!this._sessionService.user) {
            this._router.navigate([this.selectBestRoute()]);
            return false;
        }
        return true;
    }

    selectBestRoute(): string {
        if (!this._sessionService.user) {
            return '/account/login';
        }

        if (this._permissionChecker.isGranted('Pages.Users')) {
            return '/app/home';
        }
        return '/app/home';
    }
}