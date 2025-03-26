import React, { useState } from "react";
import { useUpdateAccountMutation } from "./api/expensetrackerApi";

function UpdateAccount({account1, updateIsUpdatingFlag}) {

  const [updateAccountMutation, { error, isError, isSuccess, status }] = useUpdateAccountMutation();
  const [updatedAccountName, setUpdatedAccountName] = useState(account1.name);

  const updateAccount = async (e) => {
    e.preventDefault();

    updateAccountMutation({
      id: account1.id,
      name: updatedAccountName
    });
    
  }


  let message;

  if (isError) {
    message = error?.data;
  }

  if (isSuccess && status == "fulfilled") {
    message = "Successfully Updated";
    //setUpdatedAccountName("");
    updateIsUpdatingFlag(false);    
  }

  return (
    <div>
      <form>
        <label >Account Name</label>
        <input type="text" id="accountname" name="accountname" value={updatedAccountName} onChange={(e) => setUpdatedAccountName(e.target.value)}></input>
        <button onClick={updateAccount}>Update</button>
        {message}


      </form>

    </div>

  );
}

export default UpdateAccount;