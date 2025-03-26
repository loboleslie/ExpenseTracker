import React, { useState } from "react";
import { useAddAccountMutation } from "./api/expensetrackerApi";

function AddAccount() {

  const [addAccountMutation, { error, isError, isSuccess, status }] = useAddAccountMutation();
  const [newAccountName, setNewAccountName] = useState("");

  const addAccount = async (e) => {
    e.preventDefault();

    addAccountMutation({
      Name: newAccountName
    });
    setNewAccountName("");    
  }


  let message;

  if (isError) {
    message = error?.data;
  }

  if (isSuccess && status == "fulfilled") {
    message = "Successfully Added";
  }

  return (
    <div>
      <form>
        <label >Account Name</label>
        <input type="text" id="accountname" name="accountname" value={newAccountName} onChange={(e) => setNewAccountName(e.target.value)}></input>
        <button onClick={addAccount}>Add</button>
        {message}


      </form>

    </div>

  );
}

export default AddAccount;