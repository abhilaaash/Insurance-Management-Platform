import { Component } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { ActivatedRoute } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CustomerModalComponent } from '../customer-modal/customer-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Customer } from '../model/Customer';
import { ModalCustomerComponent } from '../modal-customer/modal-customer.component';
import { HttpClient } from '@angular/common/http';



@Component({
  selector: 'app-customer-document',
  templateUrl: './customer-document.component.html',
  styleUrls: ['./customer-document.component.css']
})
export class CustomerDocumentComponent {
  customerID: any
  documents: any
  headers: any
  currentPage: number = 1
  pageSizes: number[] = [5, 10, 15];
  totalDocumentCount = 0;
  pageSize = this.pageSizes[0];
  constructor(private http: HttpClient
    , private customer: CustomerService, private activatedroute: ActivatedRoute, private modalService: NgbModal) { }
  isEmployee = false
  isAdmin = false
  jwtHelper = new JwtHelperService()
  customerId: number = 0
  CustomerCount: number = 0
  ngOnInit() {

    const customerIdString = localStorage.getItem('customerId');
    if (customerIdString !== null) {
      this.customerId = +customerIdString; // Use the + operator to convert the string to a number
    } else {
    }
    this.customerID = this.activatedroute.snapshot.paramMap.get('id');
    const decodedToken = this.jwtHelper.decodeToken(localStorage.getItem('token')!);

    const role: string = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if (role === 'Employee') {
      this.isEmployee = true
    }
    else {
      this.isAdmin = true
    }
    this.getDocuments()
  }


  getDocuments() {


    this.customer.getDocument(this.currentPage, this.pageSize).subscribe({
      next: (response) => {
        console.log(response)
        const paginationHeader = response.headers.get('X-Pagination');
        console.log(paginationHeader);
        const paginationData = JSON.parse(paginationHeader!);
        console.log(paginationData.TotalCount);

        this.totalDocumentCount = paginationData.TotalCount;
        this.documents = response.body;
        this.CustomerCount = this.documents.length
        // console.log(this.documents)
        //this.updatePaginatedEmployees();

      }
    })
  }



  get pageNumbers(): number[] {
    return Array.from({ length: this.pageCount }, (_, i) => i + 1);
  }
  get pageCount(): number {
    return Math.ceil(this.totalDocumentCount / this.pageSize);
  }



  changePage(page: number) {

    this.currentPage = page;
    this.getDocuments();
  }
  calculateSRNumber(index: number): number {
    return (this.currentPage - 1) * this.pageSize + index + 1;
  }
  onPageSizeChange(event: Event) {
    this.pageSize = +(event.target as HTMLSelectElement).value;
    this.getDocuments();
  }
  searchQuery: string | number = '';

  onSearch() {
    console.log(typeof this.searchQuery)
    this.customer.getDocument(this.currentPage, this.pageSize, this.searchQuery).subscribe({
      next: (response) => {

        const paginationHeader = response.headers.get('X-Pagination');
        console.log(paginationHeader);
        const paginationData = JSON.parse(paginationHeader!);
        console.log(paginationData.TotalCount);

        this.totalDocumentCount = paginationData.TotalCount;
        this.documents = response.body;
        //this.updatePaginatedEmployees();

      }
    })
  }
  downloadDocument(doc: any) {
    this.customer.downloadFile(doc.documentId).subscribe((response) => {
      const contentDispositionHeader = response.headers.get('Content-Disposition');
      const fileName = contentDispositionHeader?.split(';')[1].split('filename=')[1].trim();

      if (response.body instanceof Blob && fileName) {
        // Create a Blob object from the response data
        const blob = new Blob([response.body], { type: response.body.type });

        // Trigger a file download
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = fileName;
        a.click();
        window.URL.revokeObjectURL(url);
      } else {
        console.error('Response body is not a Blob.');
      }
    });
  }
  updateDocumentStatus(data: any) {
    this.customer.updateCustomerDocuments(data.documentId).subscribe(
      (res) => {
        alert("Updated Successfully");
        window.location.reload()
      },
      (err) => {
        alert("Something went wrong");
        window.location.reload()
      }
    )
  }
  customerr :any;
  customerrr:any
  fetchCustomerDetails(customerId: number) {
    
    this.http.get<Customer>(`https://localhost:7124/api/Customer/GetById?Id=${customerId}`).subscribe((data) => {
      this.customerr = data;
      console.log(data);
      this.openCustomerModal(this.customerr)

    });
  }
  fetchCustomerDetailss(customerId: number) {
    
    this.http.get<Customer>(`https://localhost:7124/api/Customer/GetById?Id=${customerId}`).subscribe((data) => {
      this.customerrr = data;
      console.log(data);
      

    });
  }
  openCustomerModal(customer: any) {
    const modalRef = this.modalService.open(ModalCustomerComponent, { centered: true, size: 'sm' });
    modalRef.componentInstance.customer = customer;
  }

}
