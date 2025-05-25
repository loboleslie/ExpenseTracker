import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const accountExpenseTrackerApi = createApi({
  reducerPath: "accountexpensetracker",
  baseQuery: fetchBaseQuery({ baseUrl: "https://localhost:44397/" }),
  tagTypes: ["Accounts"],
  endpoints: (builder) => ({
    getAllAccounts: builder.query({
      query: ({ pageNumber, pageSize, searchTerm }) => `account?pageNumber=${pageNumber}&pageSize=${pageSize}&searchTerm=${searchTerm}`,
      providesTags: ({ pageNumber, pageSize, searchTerm }) => {
        return [{ type: "Accounts", pageNumber: pageNumber, pageSize: pageSize, searchTerm: searchTerm }]
      }
    }),
    addAccount: builder.mutation({
      query: (account) => ({
        url: "account",
        method: "POST",
        body: account,
      }),
      invalidatesTags: ["Accounts"],
    }),
    updateAccount: builder.mutation({
      query: (account) => ({
        url: `account/${account.id}`,
        method: "PUT",
        body: account,
      }),
      invalidatesTags: ["Accounts"],
    }),
    deleteAccount: builder.mutation({
      query: ({ id }) => ({
        url: `account?id=${id}`,
        method: "DELETE"
      }),
      invalidatesTags: ["Accounts"],
    }),
  })
});

export const {
  useGetAllAccountsQuery,
  useAddAccountMutation,
  useUpdateAccountMutation,
  useDeleteAccountMutation
} = accountExpenseTrackerApi;