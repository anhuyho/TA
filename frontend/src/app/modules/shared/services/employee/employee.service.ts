import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParamsOptions} from "@angular/common/http";
import {BehaviorSubject, Observable, tap} from "rxjs";
import {Employee} from "../../../../models/employee";
import {CacheService} from "../cache/cache.service";
import {CACHE_KEYS} from "../../../../models/cache-keys";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private baseUrl: string = "https://localhost:44396/api";

  constructor(private client: HttpClient, private cache: CacheService) { }

  public getEmployees(): Observable<Employee[]> {
    const employees = this.cache.getData(CACHE_KEYS.EMPLOYEES);
    if (employees) {
      return new BehaviorSubject<Employee[]>(JSON.parse(employees));
    }
    return this.client.get<Employee[]>(this.baseUrl + "/employees").pipe(
      tap(v => this.cache.saveData(CACHE_KEYS.EMPLOYEES, JSON.stringify(v)))
    );
  }

  public getEmployee(id: string): Observable<Employee> {
    return this.client.get<Employee>(this.baseUrl + "/employees/" + id);
  }

  public createEmployee(body: any): Observable<Employee> {
    this.cache.removeData(CACHE_KEYS.EMPLOYEES);
    return this.client.post<Employee>(this.baseUrl + "/employees", body);
  }

  public updateEmployee(id: string, body: any): Observable<any> {
    this.cache.removeData(CACHE_KEYS.EMPLOYEES);
    return this.client.put<any>(this.baseUrl + "/employees/" + id, body);
  }

  public deleteEmployee(id: string): Observable<any> {
    this.cache.removeData(CACHE_KEYS.EMPLOYEES);
    return this.client.delete(this.baseUrl + "/employees/" + id);
  }
}
