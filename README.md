 # Money Tracker

## Overview

Money Tracking is an open source application, 640KB in size, designed using .NET 5.0 technology stack to help you manage your transactions and account book.

## Getting Started

- To run the project locally, clone it from the repository and restore NuGet packages:
  ```
  git clone https://github.com/moneytracking/MoneyTracking.git
  cd MoneyTracking
  dotnet restore
  ```
- For more information about using .NET, see [here](https://docs.microsoft.com/en-us/dotnet/)

## Features

- Manage transactions by adding, removing and displaying the records
- Customisable logos for Money Tracker
- Support for multiple selection of transactions for removal

## Usage

### 1. Add Transactions

You can add a transaction by creating a new instance of the `Transaction` class in the main method or in the `ConsoleHelper.AskMultiSelection()` method.

- In the main method:
  ```
  var account = new Account();
  var transaction1 = new Transaction("Personal", "2023-05-17", 100, "Grocery", account);
  account.Transactions.Add(transaction1);
  ```
- In the `ConsoleHelper.AskMultiSelection()` method:
  ```
  public static void AskMultiSelection(Account account)
  {
  //...
  }
  var transaction2 = new Transaction("Personal", "2023-05-18", 50, "Gasoline", account);
  account.Transactions.Add(transaction2);
  ```

### 2. Remove Transactions

Use the `ConsoleHelper.AskMultiSelection()` method to remove transactions from a specific account:

```
var account = new Account();
//...
ConsoleHelper.AskMultiSelection(account);
```

## Logos

- Display Money Tracker logo:
  ```
  ConsoleHelper.DisplayMoneyTrackerLogo()
  ```
- Display Money Tracker logo with cricket font:
  ```
  ConsoleHelper.DisplayMoneyTrackerLogoOther()
  ```

### Troubleshooting

If you encounter any issues, please report them on the issue tracking system in our repository.

[source_id]Taken from TransactionBuilder.cs and ConsoleHelper.cs files[/] # Money Tracker

## Overview

Money Tracking is an open source application designed using .NET 5.0 technology stack to help you manage your transactions and account book. The program is approximately 640KB in size.

## Getting Started

To run the project locally, follow these steps:

1. Clone it from the repository: `git clone https://github.com/moneytracking/MoneyTracking.git`
2. Navigate to the project directory: `cd MoneyTracking`
3. Restore NuGet packages: `dotnet restore`

For more information about using .NET, see [here](https://docs.microsoft.com/en-us/dotnet/).

## Features

- Manage transactions by adding, removing and displaying the records
- Customisable logos for Money Tracker
- Support for multiple selection of transactions for removal

## Usage

### 1. Add Transactions

You can add a transaction by creating a new instance of the `Transaction` class in the main method or in the `ConsoleHelper.AskMultiSelection()` method.

- In the main method:
  ```csharp
  var account = new Account();
  var transaction1 = new Transaction("Personal", "2023-05-17", 100, "Grocery", account);
  account.Transactions.Add(transaction1);
  ```
- In the `ConsoleHelper.AskMultiSelection()` method:
  ```csharp
  var transaction2 = new Transaction("Personal", "2023-05-18", 50, "Gasoline", account);
  account.Transactions.Add(transaction2);
  ```

### 2. Remove Transactions

Use the `ConsoleHelper.AskMultiSelection()` method to remove transactions from a specific account:

```csharp
var account = new Account();
//...
ConsoleHelper.AskMultiSelection(account);
```

## Logos

- Display Money Tracker logo:
  ```csharp
  ConsoleHelper.DisplayMoneyTrackerLogo();
  ```
- Display Money Tracker logo with cricket font:
  ```csharp
  ConsoleHelper.DisplayMoneyTrackerLogoOther();
  ```

### Troubleshooting

If you encounter any issues, please report them on the issue tracking system in our repository.

[//] # Money Tracker

## Overview

moneytracking is an open source application designed using .NET 5.0 technology stack to help you manage your transactions and account book. The program is approximately 640 KB in size.

## Getting Started

To run the project locally, follow these steps:

1. Clone it from the repository: `git clone https://github.com/moneytracking/MoneyTracking.git`
2. Navigate to the project directory: `cd MoneyTracking`
3. Restore NuGet packages: `dotnet restore`

For more information about using .NET, see [here](https://docs.microsoft.com/en-us/dotnet/).

## Features

- Manage transactions by adding, removing and displaying the records
- Customisable logos for Money Tracker
- Support for multiple selection of transactions for removal

## Usage

### 1. Add Transactions

You can add a transaction by creating a new instance of the `Transaction` class in the main method or in the `ConsoleHelper.AskMultiSelection()` method.

- In the main method:
  ```csharp
  var account = new Account();
  var transaction1 = new Transaction("Personal", "2023-05-17", 100, "Grocery", account);
  account.Transactions.Add(transaction1);
  ```
- In the `ConsoleHelper.AskMultiSelection()` method:
  ```csharp
  var transaction2 = new Transaction("Personal", "2023-05-18", 50, "Gasoline", account);
  account.Transactions.Add(transaction2);
  ```

### 2. Remove Transactions

Use the `ConsoleHelper.AskMultiSelection()` method to remove transactions from a specific account:

```csharp
var account = new Account();
//...
ConsoleHelper.AskMultiSelection(account);
```

## Logos

- Display Money Tracker logo:
  ```csharp
  ConsoleHelper.DisplayMoneyTrackerLogo();
  ```
- Display Money Tracker logo with cricket font:
  ```csharp
  ConsoleHelper.DisplayMoneyTrackerLogoOther();
  ```

### Troubleshooting

If you encounter any issues, please report them on the issue tracking system in our repository.

[//] # Money Tracker

## Overview

moneytracking is an open source application designed using .NET 5.0 technology stack to help you manage your transactions and account book. The program is approximately 640 KB in size.

## Getting started

To run the project locally, follow these steps:

1. Clone it from the repository: `git clone https://github.com/moneytracking/MoneyTracking.git`
2. Navigate to the project directory: `cd MoneyTracking`
3. Restore NuGet packages: `dotnet restore`

For more information about using .NET, see [here](https://docs.microsoft.com/en-us/dotnet/).

## Features

- Manage transactions by adding, removing and displaying the records
- Customisable logos for Money Tracker
- Support for multiple selection of transactions for removal

## Usage

### 1. Add Transactions

You can add a transaction by creating a new instance of the `Transaction` class in the main method or in the `ConsoleHelper.AskMultiSelection()` method.

- In the main method:
  ```csharp
  var account = new Account();
  var transaction1 = new Transaction("Personal", "2023-05-17", 100, "Grocery", account);
  account.Transactions.Add(transaction1);
  ```
- In the `ConsoleHelper.AskMultiSelection()` method:
  ```csharp
  var transaction2 = new Transaction("Personal", "2023-05-18", 50, "Gasoline", account);
  account.Transactions.Add(transaction2);
  ```

### 2. Remove Transactions

Use the `ConsoleHelper.AskMultiSelection()` method to remove transactions from a specific account:

```csharp
var account = new Account();
//...
ConsoleHelper.AskMultiSelection(account, "Select transaction(s) to remove");
```

## Logos

- Display Money Tracker logo:
  ```csharp
  ConsoleHelper.DisplayMoneyTrackerLogo();
  ```
- Display Money Tracker logo with cricket font:
  ```csharp
  ConsoleHelper.DisplayMoneyTrackerLogoWithCricketFont();
  ```

### Troubleshooting

If you encounter any issues, please report them on the issue tracking system in our repository.

[//] # moneytracking

## Overview

moneytracking is an open source application designed using .NET 5.0 technology stack to help manage transactions and account book. The program is approximately 640 KB in size.

## Getting started

1. Clone the project from the repository: `git clone https://github.com/moneytracking/MoneyTracking.git`
2. Navigate to the project directory: `cd MoneyTracking`
3. Restore NuGet packages: `dotnet restore`

## Features

- Add transactions
- Remove transactions
- Display transactions
- Customizable logos

### Transactions

#### Add

You can add a transaction by creating a new instance of the `Transaction` class in the main method or in the `ConsoleHelper.AskMultiSelection()` method.

```csharp
var account = new Account();

// In Main Method
var transaction1 = new Transaction("Personal", "2023-05-17", 100, "Grocery", account);
account.Transactions.Add(transaction1);

// In ConsoleHelper.AskMultiSelection() method
var transaction2 = new Transaction("Personal", "2023-05-18", 50, "Gasoline", account);
account.Transactions.Add(transaction2);
```

#### Remove

Use the `ConsoleHelper.AskMultiSelection()` method to remove transactions from a specific account:

```csharp
var account = new Account();
//...
ConsoleHelper.AskMultiSelection(account, "Select transaction(s) to remove");
```

### Logos

- Display Money Tracker logo:
  ```csharp
  ConsoleHelper.DisplayMoneyTrackerLogo();
  ```
- Display Money Tracking Logo with cricket font:
  ```csharp
  ConsoleHelper.DisplayMoneyTrackerLogoWithCricketFont();
  ```

## Troubleshooting

If you encounter any issues, please report them on the issue tracking system in our repository.
