import { User, Post } from "./index";

export declare module Deal {
  export type Deal = {
    id: number;
    postId: number;
    bookTakerId: number;
    dealStatus: string;

    bookTaker: User.User | null;
    post: Post.Post | null;
  };
}
