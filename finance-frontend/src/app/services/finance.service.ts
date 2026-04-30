import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  TransactionResponseDto,
  UserResponseDto,
  TransactionCreateDto,
  TransactionUpdateDto
} from '../models/finance.models';

@Injectable({
  providedIn: 'root'
})
export class FinanceService {
  private apiUrl = 'http://localhost:5268/api';

  constructor(private http: HttpClient) {}

  // User
  getUser(id: string): Observable<UserResponseDto> {
    return this.http.get<UserResponseDto>(`${this.apiUrl}/users/${id}`);
  }

  // Transactions
  getUserTransactions(userId: string): Observable<TransactionResponseDto[]> {
    return this.http.get<TransactionResponseDto[]>(`${this.apiUrl}/transactions/user/${userId}`);
  }

  createTransaction(dto: TransactionCreateDto): Observable<TransactionResponseDto> {
    return this.http.post<TransactionResponseDto>(`${this.apiUrl}/transactions`, dto);
  }

  deleteTransaction(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/transactions/${id}`);
  }

  updateTransaction(id: string, dto: TransactionUpdateDto): Observable<TransactionResponseDto> {
    return this.http.put<TransactionResponseDto>(`${this.apiUrl}/transactions/${id}`, dto);
  }
}
