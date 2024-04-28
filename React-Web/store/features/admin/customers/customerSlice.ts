import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {getCustomers} from "@/store/services/customerService";

export interface ICustomer {
    customerId: string
    firstName: string
    lastName: string
    phoneNumber: string
}

interface ICustomerState {
    customers: ICustomer[] | null
    loading: boolean
    error: string | null
    totalCount: number;
    limit: number;
    activePage: number;
}

const initialState: ICustomerState = {
    customers: null,
    loading: true,
    error: null,
    totalCount: 0,
    limit: 6,
    activePage:  1,
}

const customerSlice = createSlice({
    name: "customer",
    initialState: initialState,
    reducers: {},
    extraReducers: builder => {
        builder.addCase(getCustomers.pending, (state) => {
            state.loading = true;
            state.customers = null;
            state.error = null

        })
            .addCase(getCustomers.fulfilled, (state, action) => {
                state.loading = false;
                // @ts-ignore
                state.customers = action.payload
                state.error = null
            })
            .addCase(getCustomers.rejected, (state, action) => {
                state.loading = false;
                state.customers = null
                state.error = action.payload as string;
            });
    }
})



export default customerSlice.reducer;