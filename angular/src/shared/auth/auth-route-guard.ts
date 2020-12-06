// import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AppSessionService } from '@shared/session/app-session.service';
import { PermissionCheckerService } from 'abp-ng2-module';
// import { AppSessionService } from '@shared/common/session/app-session.service';

@Injectable()
export class AppRouteGuard implements CanActivate {

    constructor(
        private _permissionChecker: PermissionCheckerService,
        private _router: Router,
        private _sessionService: AppSessionService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

        if (route.queryParams['ss'] && route.queryParams['ss'] === 'true') {
            return true;
        }

        console.log(this._sessionService.user, route);
        // debugger
        if (!this._sessionService.user) {
            if (route['_routerState'].url === '/account/login') {
                return true;
            }
            else {
                this._router.navigate(['/account/login']);
                return false;
            }
        }

        this._router.navigate([this.selectBestRoute()]);

        return false;
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
