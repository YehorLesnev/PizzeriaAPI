"use client"
import {configureStore} from "@reduxjs/toolkit";
import authReducer from "@/store/features/auth/authSlice"
import productReducer from "@/store/features/product/productSlice"
import customerReducer from "@/store/features/admin/customers/customerSlice"
import staffReducer from "@/store/features/admin/staff/staffSlice"
import ordersReducer from "@/store/features/orders/ordersSlice";
export const store = configureStore({
    reducer: {
        user: authReducer,
        product: productReducer,
        customer: customerReducer,
        staff: staffReducer,
        order: ordersReducer
    },
});

export type RootState = ReturnType<typeof store.getState>;

export type AppDispatch = typeof store.dispatch;