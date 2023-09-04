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
  private baseToken: string = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJleHAiOjMzMjUwNzI5NDg1LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjQ0Mzk2LyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQzOTYvIn0.7iIZT-VuxxLWYzv0-1Z7FTsomPZqRv3r2VspyhN_w4s";
  private basehttpOption =  {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': '*',
      'Content-Type':  'application/json; charset=UTF-8',
      'Authorization': 'Bearer '+ this.baseToken
    })
  };

  constructor(private client: HttpClient, private cache: CacheService) { }

  public getEmployees(): Observable<Employee[]> {
    const employees = this.cache.getData(CACHE_KEYS.EMPLOYEES);
    if (employees) {
      return new BehaviorSubject<Employee[]>(JSON.parse(employees));
    }
    return this.client.get<Employee[]>(this.baseUrl + "/employees", this.basehttpOption).pipe(
      tap(v => this.cache.saveData(CACHE_KEYS.EMPLOYEES, JSON.stringify(v)))
    );
  }

  public getEmployee(id: string): Observable<Employee> {
    return this.client.get<Employee>(this.baseUrl + "/employees/" + id, this.basehttpOption);
  }

  public createEmployee(body: any): Observable<Employee> {
    this.cache.removeData(CACHE_KEYS.EMPLOYEES);
    return this.client.post<Employee>(this.baseUrl + "/employees", body, this.basehttpOption);
  }

  public updateEmployee(id: string, body: any): Observable<any> {
    this.cache.removeData(CACHE_KEYS.EMPLOYEES);
    return this.client.put<any>(this.baseUrl + "/employees/" + id, body, this.basehttpOption);
  }

  public deleteEmployee(id: string): Observable<any> {
    this.cache.removeData(CACHE_KEYS.EMPLOYEES);
    return this.client.delete(this.baseUrl + "/employees/" + id, this.basehttpOption);
  }
}
