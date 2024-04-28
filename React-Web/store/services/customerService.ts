import {createAsyncThunk} from "@reduxjs/toolkit";
import {instance} from "@/api/axiosInstance";
import Cookies from "js-cookie";
import {FetchProductsParams} from "@/store/services/productService";

export const getCustomers = createAsyncThunk<any, FetchProductsParams>("customer/getCustomers", async ({ pageNumber, pageSize}) => {
    try {
        const params: FetchProductsParams = {};
        if (pageNumber) params.pageNumber = pageNumber;
        if (pageSize) params.pageSize = pageSize;
        const response = await instance.get("/Customers", {params});
        console.log(response.data)
        return response.data;
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
});

export const getIngredients =  async () => {
    try {
        const params: FetchProductsParams = {};
        const response = await instance.get("/Ingredients", );
        return response.data;
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const getCustomerCount = async ({ pageNumber, pageSize, itemCategory}: FetchProductsParams) => {
    try {
        const params: FetchProductsParams = {};
        if (pageNumber) params.pageNumber = pageNumber;
        if (pageSize) params.pageSize = pageSize;
        const response = await instance.get("/Customers", {params});

        return response.data;
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}

export const getOrdersCount = async ({ pageNumber, pageSize, userEmail}: FetchProductsParams) => {
    try {
        const params: FetchProductsParams = {};
        if (pageNumber) params.pageNumber = pageNumber;
        if (pageSize) params.pageSize = pageSize;
        const response = await instance.get(`/Orders/user/${userEmail}`, {params});

        return response.data;
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const deleteCustomer = async (id: string) => {
    try {
        await instance.delete(`/Customers/${id}`);
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}
export const deleteItem = async (id: string) => {
    console.log(id)
    try {
        await instance.delete(`/Items/${id}`);
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const updateCustomer = async (customerId: string | undefined, firstName: string, lastName: string, phoneNumber: string) => {
    try {
        await instance.put(`/Customers/${customerId}`, { firstName, lastName, phoneNumber})


    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}

export const updateItem = async (id: string | undefined, data: FormData) => {
    try {
        await instance.put(`/Items/${id}`, data)


    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}

export const postItem = async (data: FormData) => {
    console.log(data)
    try {
        await instance.post("/Items", data)


    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const postAddress = async (address1: string, city:string) => {
    try {
        const res = await instance.post("/Addresses", {address1, address2: "", zipCode: "", City: city} )
        return res.data.addressId

    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const postOrder = async (date: string, customerId: string | null, itemId: string, quantity: number, isDelivery: boolean, deliveryAddressId: string | null) => {
    try {
        await instance.post("/Orders", {date, customerId: customerId, orderItems: [{itemId, quantity}], delivery: isDelivery, deliveryAddressId})


    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const postCustomer = async (firstName: string, lastName: string, phoneNumber: string, email: string, password: string ) => {
    try {

        await instance.post(`/Customers`, {firstName, lastName, phoneNumber, email, password})


    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const postRecipe = async (payload: object) => {
    try {
        const res = await instance.post("/Recipes", payload)
        return res
    } catch (e){
        console.log(e)
    }
}



