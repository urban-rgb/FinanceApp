
// This file is like a Controller in ASP.NET

import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FinanceService } from './services/finance.service';
import { UserResponseDto, TransactionResponseDto } from './models/finance.models';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class AppComponent implements OnInit {
  userId = 'a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d'; // type is automaticly string
  user = signal<UserResponseDto | null>(null); // like a variable? in C#
  transactions = signal<TransactionResponseDto[]>([]);

  // Dependency Injection - Angular searching for FinanceService in DI Container and
  // put it in "this.financeService" field
  constructor(private financeService: FinanceService) {}

  // Similar to Start() in Unity
  ngOnInit(): void {
    this.loadUserData();
    this.loadTransactions();
  }

  // Subscribing inside loadUserData() is for:
  // At first code creating Observable object, that waiting for subscribing
  // then we are .subscribing and activating Observable object
  // and then Angular creating Query to API
  // it's like C# await, but thread just keep doing its stuff, so site can display old information for a while
  loadUserData(): void {
    this.financeService.getUser(this.userId).subscribe(data => {
      console.log('User data received:', data);
      this.user.set(data);
    });
  }

  // .subscirbe() for Observable method is like a trigger.
  // Without .subscribe method won't work, because "Why do action, if you don't care abt the result?"
  loadTransactions(): void {
    this.financeService.getUserTransactions(this.userId).subscribe(data => {
      this.transactions.set(data);
    });
  }

  // Subscribing inside deleting is for:
  // getting information from API 'bout new information and refreshing numbers on frontend
  onDelete(id: string): void {
    if (confirm('Are you sure?')) {
      this.financeService.deleteTransaction(id).subscribe(() => {
        this.loadTransactions();
        this.loadUserData();
      });
    }
  }
}
