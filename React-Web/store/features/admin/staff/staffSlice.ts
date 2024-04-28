import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {getStaff} from "@/store/services/staffService";


export interface IStaff {
    staffId: string
    firstName: string
    lastName: string
    phoneNumber: string
}

interface Istafftate {
    staff: IStaff[] | null
    loading: boolean
    error: string | null
    totalCount: number;
    limit: number;
    activePage: number;
}

const initialState: Istafftate = {
    staff: null,
    loading: true,
    error: null,
    totalCount: 0,
    limit: 6,
    activePage:  1,
}

const stafflice = createSlice({
    name: "staff",
    initialState: initialState,
    reducers: {},
    extraReducers: builder => {
        builder.addCase(getStaff.pending, (state) => {
            state.loading = true;
            state.staff = null;
            state.error = null

        })
            .addCase(getStaff.fulfilled, (state, action) => {
                state.loading = false;
                // @ts-ignore
                state.staff = action.payload
                state.error = null
            })
            .addCase(getStaff.rejected, (state, action) => {
                state.loading = false;
                state.staff = null
                state.error = action.payload as string;
            });
    }
})



export default stafflice.reducer;