import {configureStore} from "@reduxjs/toolkit";
import { accountExpenseTrackerApi} from "../api/accountExpenseTrackerApi";

export const store = configureStore({
    reducer: {
        [accountExpenseTrackerApi.reducerPath]: accountExpenseTrackerApi.reducer    
    },
    middleware:(getDefaultMiddleware) =>
        getDefaultMiddleware().concat(accountExpenseTrackerApi.middleware),
});