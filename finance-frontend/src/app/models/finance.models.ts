export enum TransactionType {
  Income = 'Income',
  Expense = 'Expense'
}

export interface UserResponseDto {
  id: string;
  name: string;
  balance: number;
}

export interface TransactionResponseDto {
  id: string;
  name: string;
  amount: number;
  type: TransactionType;
  createdAt: string;
}

export interface TransactionCreateDto {
  name: string;
  amount: number;
  type: TransactionType;
  userId: string;
}

export interface TransactionUpdateDto {
  name: string;
  amount: number;
  type: TransactionType;
}
