import {createAsyncThunk} from "@reduxjs/toolkit";
import {instance} from "@/api/axiosInstance";
import Cookies from "js-cookie";
import {FetchProductsParams} from "@/store/services/productService";

export const getStaff = createAsyncThunk<any, FetchProductsParams>("staff/getStaff", async ({ pageNumber, pageSize}) => {
    try {
        const params: FetchProductsParams = {};
        if (pageNumber) params.pageNumber = pageNumber;
        if (pageSize) params.pageSize = pageSize;
        const response = await instance.get("/Staff", {params});
        console.log(response.data)
        return response.data;
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
});


export const getStaffCount = async ({ pageNumber, pageSize}: FetchProductsParams) => {
    try {
        const params: FetchProductsParams = {};
        if (pageNumber) params.pageNumber = pageNumber;
        if (pageSize) params.pageSize = pageSize;
        const response = await instance.get("/Staff", {params});

        return response.data;
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const deleteStaff = async (id: string) => {
    console.log(id)
    try {

        await instance.delete(`/Staff/${id}`);


    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const updateStaff = async (staffId: string | undefined, firstName: string, lastName: string, phoneNumber: string, hourlyRate: string | undefined, position: string | undefined) => {
    try {
            const rate = Number(hourlyRate)
        await instance.put(`/Staff/${staffId}`, {firstName, lastName, phoneNumber, hourlyRate: rate, position})


    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const postStaff = async (firstName: string, lastName: string, phoneNumber: string, hourlyRate: string | undefined, position: string | undefined) => {
    try {
        const rate = hourlyRate?.replace(".", ",")
        await instance.post(`/Staff`, {firstName, lastName, phoneNumber, hourlyRate: rate, position})


    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


