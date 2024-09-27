import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthWithAPIService } from './auth-with-api.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AdminGuardService implements CanActivate {
  constructor(private authService: AuthWithAPIService, private router: Router) {}

  canActivate(): Observable<boolean> {
    return this.authService.isAdmin().pipe(
      map((isAdmin) => {
        if (!isAdmin) {
          this.router.navigate(['/login']);
        }
        return isAdmin;
      })
    );
  }
}
