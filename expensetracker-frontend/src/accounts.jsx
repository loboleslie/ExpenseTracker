import React, { useState } from "react";
import { useGetAllAccountsQuery, useDeleteAccountMutation } from "./api/accountExpenseTrackerApi";
import AddAccount from "./addAccount";
import UpdateAccount from "./updateAccount";


function Accounts() {
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(2);
  const [pageSize] = useState(10);
  const [searchTerm, setSearchTerm] = useState("");
  const [isUpdating, setIsUpdating] = useState(false);
  const [selectAccount, setSelectAccount] = useState(null);

  const [deleteAccount] = useDeleteAccountMutation();


  const { data, isLoading, isSuccess, isError } =
    useGetAllAccountsQuery({
      pageNumber: currentPage,
      pageSize: pageSize,
      searchTerm: searchTerm
    }, {
      refetchOnMountOrArgChange: true
    });

  const handleNextPage = () => {
    if (currentPage < totalPages) {
      setCurrentPage(currentPage + 1);
    }
    console.log(currentPage + 1);
  }

  const handlePreviousPage = () => {
    if (currentPage > 1) {
      setCurrentPage(currentPage - 1);
    }
  }

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
    setCurrentPage(1);
  }

  const selectRecord = (record) => {
    
    alert(record.name);
    setIsUpdating(true);
    setSelectAccount({ id: record.id, name: record.name });
  }

  const updateIsUpdatingFlag = (flag) =>
  {
    setIsUpdating(flag);
  }


  const deleteRecord = (record) => {

    deleteAccount({ id: record.id});

  }


  let content;
  let addOrUpdateAccount;
  let errormessage;

  if(isSuccess){
    console.log(isUpdating);

    if (!isUpdating) {
      addOrUpdateAccount = <AddAccount />
    }
    else {
      addOrUpdateAccount = <UpdateAccount account1={selectAccount} updateIsUpdatingFlag={updateIsUpdatingFlag}  />
    }
  }

  


  if (isLoading) {
    content = <p>Loading...</p>;
  } else if (isSuccess) {
    content = <table>
      <tbody>
      <tr>
        <th>ID</th>
        <th>Name</th>
        <th> </th>
      </tr>
      {data.result.item.accounts.map((account) => (
        <tr key={account.id}>
          <td>{account.id}</td>
          <td>{account.name}</td>
          <td>
            <button onClick={(e) => { e.preventDefault(); selectRecord(account);}}>Select</button>&nbsp;
            <button onClick={(e) => { e.preventDefault(); deleteRecord(account);}}>Delete</button></td>
        </tr>
      ))}


      <tr>
        <td><button onClick={handlePreviousPage} disabled={currentPage === 1}>Previous</button></td>
        <td>Page {currentPage} of {totalPages}</td>
        <td><button onClick={handleNextPage} disabled={currentPage === totalPages}>Next</button></td>
      </tr>
      </tbody>
    </table>
  };

  return (
    <>
      <h1>Accounts</h1>
      <input type="text" value={searchTerm} onChange={handleSearchChange} placeholder="Search by name" />
      <div>
        {addOrUpdateAccount}
        {content}
      </div>
    </>
  );
}

export default Accounts
