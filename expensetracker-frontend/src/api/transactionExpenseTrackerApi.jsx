import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const transactionExpenseTracker = createApi({
    reducerPath: "transactionexpensetracker",
    baseQuery: fetchBaseQuery({ baseUrl: "https://localhost:44397/" }),
    tagTypes: ["Transactions"],
    endpoints: (builder) => ({
        getAllTransactions: builder.query({
            query: ({ pageNumber, pageSize, searchTerm }) => `transaction?pageNumber=${pageNumber}&pageSize=${pageSize}&searchTerm=${searchTerm}`,
            providesTags: ({ pageNumber, pageSize, searchTerm }) => {
                return [{ type: "Transactions", pageNumber: pageNumber, pageSize: pageSize, searchTerm: searchTerm }]
            }
        }),
        addTransaction: builder.mutation({
            query: (transaction) => ({
                url: "transaction",
                method: "POST",
                body: transaction,
            }),
            invalidatesTags: ["Transactions"],
        }),
        updateTransaction: builder.mutation({
            query: (transaction) => ({
                url: `transaction/${transaction.id}`,
                method: "PUT",
                body: transaction,
            }),
            invalidatesTags: ["Transactions"],
        }),
        deleteTransaction: builder.mutation({
            query: ({ id }) => ({
                url: `transaction?id=${id}`,
                method: "DELETE"
            }),
            invalidatesTags: ["Transactions"],
        }),
    })
});

export const {
  useGetAllTransactionsQuery,
  useAddTransactionMutation,
  useUpdateTransactionMutation,
  useDeleteTransactionMutation
} = transactionExpenseTrackerApi;