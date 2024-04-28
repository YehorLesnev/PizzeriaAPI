import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {getOrders, getProducts} from "@/store/services/productService";

export interface IItem {
    itemId: string
    itemName:string
    itemCategory: string
    itemSize: string
    itemPrice: number
}

export interface IOrders {
    orderId: string
    date: string
    staffId: string
    customerId: string
    orderItems: [
        {
            item: IItem
            quantity: number
        }
    ],
    total: number,
    delivery: boolean,
    deliveryAddressId: string
}

interface IOrderState {
    orders: IOrders | null
    loading: boolean,
    error: string | null,
}

const initialState: IOrderState = {
    orders: null,
    loading: true,
    error: null,
}

const ordersSlice = createSlice({
    name: "orders",
    initialState: initialState,
    reducers: {

    },
    extraReducers: builder => {
        builder.addCase(getOrders.pending, (state) => {
            state.loading = true;
            state.orders = null;
            state.error = null

        })
            .addCase(getOrders.fulfilled, (state, action) => {
                console.log(action.payload, "342")
                state.loading = false;
                // @ts-ignore
                state.orders = action.payload
                state.error = null
            })
            .addCase(getOrders.rejected, (state, action) => {
                state.loading = false;
                state.orders = null
                state.error = action.payload as string;
            });
    }
})



export default ordersSlice.reducer;