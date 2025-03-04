import React, { useState, useEffect} from "react"

function Accounts() {

  const [accounts, setAccounts] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(2);
  const [pageSize] = useState(2);
  const [searchTerm, setSearchTerm] = useState("");


  useEffect(() => {
    fetchAccounts();
  },[currentPage, searchTerm]);

  const fetchAccounts = async () => {
    const response = await fetch(`https://localhost:44397/Account?pageNumber=${currentPage}&pageSize=${pageSize}&searchTerm=${searchTerm}`);

    const data = await response.json();
    setAccounts(data.result.item.accounts);
    console.log(data);
  };

  const handleNextPage = () => {
    if(currentPage < totalPages){
      setCurrentPage(currentPage +1);
    }
  }

  const handlePreviousPage = () => {
    if(currentPage > 1){
      setCurrentPage(currentPage - 1);
    }
  }

  const handleSearchChange = (event : any) => {
    setSearchTerm(event.target.value);
    setCurrentPage(1);
  }

  return (
    <>
    <h1>Accounts</h1>
    <input type="text" value={searchTerm} onChange={handleSearchChange} placeholder="Search by name"/>
    <div>
    <table>
  <tr>
    <th>ID</th>
    <th>Name</th>
    <th> </th>
  </tr>
  {accounts.map((account : any) => (
     <tr>
     <td>{account.id}</td>
     <td>{account.name}</td>
     <td><button>Add</button>&nbsp;
     <button>Update</button>&nbsp;
     <button>Delete</button></td>
   </tr>
  ))}
 
  <tr>
  <td><button onClick={handlePreviousPage} disabled={currentPage === 1}>Previous</button></td>
    <td>Page {currentPage} of {totalPages}</td>
    <td><button onClick={handleNextPage} disabled={currentPage === totalPages}>Next</button></td>
  </tr>
</table>
    </div>
    </>
  )
}

export default Accounts
