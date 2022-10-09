import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { Employee } from 'src/app/models/Employee.model';
import { EmployeesService } from 'src/app/services/employees.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {

  // property form 
  addEmpForm!: FormGroup;

  //local public variable of type Employee
  addEmployeeRequest: Employee = {
    id: '',
    name: '',
    email: '',
    phone: 0,
    salary: 0,
    //salary: this.addEmpForm.value.salary,
    department: ''
  };

  constructor(private employeeService: EmployeesService,
    private router: Router,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.addEmpForm = this.formBuilder.group({
      Name: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      Phone: ['', [Validators.required]],
      Salary: ['', [Validators.required]],
      Department: ['', [Validators.required]]
    });
  }

  addEmployee() {
    this.employeeService.addEmployee(this.addEmployeeRequest).subscribe({
      next: (x) => {
        alert("Employee added successfully.");
        this.router.navigate(['employees']);
      },
      error: (response) =>{
        console.log(response);
      }
    });
  }
}
