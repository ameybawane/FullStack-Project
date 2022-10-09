import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from 'src/app/models/Employee.model';
import { EmployeesService } from 'src/app/services/employees.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  // form property  
  editEmpForm!: FormGroup;

  //local public variable of type Employee
  employeeDetails: Employee = {
    id: '',
    name: '',
    email: '',
    phone: 0,
    salary: 0,
    department: ''
  };
  constructor(private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeesService,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.editEmpForm = this.formBuilder.group({
      Id: [[Validators.required]],
      Name: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      Phone: ['', [Validators.required]],
      Salary: ['', [Validators.required]],
      Department: ['', [Validators.required]]
    })

    // get id from URL
    const id = this.route.snapshot.paramMap.get('id');
    //console.log(id)
    if (id != null) {
      this.employeeService.getEmployee(id)
        .subscribe({
          next: (x) => {
            this.employeeDetails = x;
          }
        });
    }
  }

  updateEmployee() {
    this.employeeService.updateEmployee(this.employeeDetails.id, this.employeeDetails)
      .subscribe((x) => {
        this.router.navigate(['employees']);
      });
  }

  deleteEmployee(id: string) {
    this.employeeService.deleteEmployee(id)
      .subscribe((x) => {
        this.router.navigate(['employees']);
      });
  }
}
