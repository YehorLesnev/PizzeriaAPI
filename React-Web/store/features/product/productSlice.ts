import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {getProducts} from "@/store/services/productService";

export interface IProduct {
    itemId: string
    itemName: string
    itemCategory: string
    itemSize: string
    itemPrice: number
    imagePath: string
    recipeId: string
}

interface IProductState {
    products: IProduct[] | null
    loading: boolean
    error: string | null
    totalCount: number;
    limit: number;
    activePage: number;
}

const initialState: IProductState = {
    products: null,
    loading: true,
    error: null,
    totalCount: 5,
    limit: 8,
    activePage:  1,
}

const productSlice = createSlice({
    name: "product",
    initialState: initialState,
    reducers: {
        setActivePage(state, action: PayloadAction<number>) {
            state.activePage = action.payload;
        },
        setTotalCount(state, action){
            state.totalCount = action.payload
        }
    },
    extraReducers: builder => {
        builder.addCase(getProducts.pending, (state) => {
            state.loading = true;
            state.products = null;
            state.error = null

        })
            .addCase(getProducts.fulfilled, (state, action) => {
                state.loading = false;
                // @ts-ignore
                state.products = action.payload
                state.error = null
            })
            .addCase(getProducts.rejected, (state, action) => {
                state.loading = false;
                state.products = null
                state.error = action.payload as string;
            });
    }
})


export const {setActivePage, setTotalCount} = productSlice.actions

export default productSlice.reducer;