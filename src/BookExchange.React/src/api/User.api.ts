import { Common, Book, Request, Deal } from "@app/types";
import { UserStats } from "@Pages/UserProfile/StatisticsBar/models/UserStats";
import { ApiBase } from "@utils/api/ApiBase";
import { ApiResponse } from "@utils/api/ApiResponse";

export class UserApi extends ApiBase {
  public static async getUserStats(id: number): Promise<ApiResponse<UserStats>> {
    return await UserApi.get<UserStats>(`/user/${id}/stats`, true);
  }

  public static async getWishedBooks(
    userId: number,
    pageSize: number,
    page: number
  ): Promise<ApiResponse<Common.PaginatedResult<Book.Book>>> {
    return await UserApi.get<Common.PaginatedResult<Book.Book>>(
      `/user/${userId}/books/wished?pageSize=${pageSize}&pageNumber=${page}`,
      true
    );
  }

  public static async getRequestsFromUser(
    userId: number,
    pageSize: number,
    page: number
  ): Promise<ApiResponse<Common.PaginatedResult<Request.Request>>> {
    return await UserApi.get<Common.PaginatedResult<Request.Request>>(
      `/user/${userId}/requests/from?pageSize=${pageSize}&pageNumber=${page}`,
      true
    );
  }

  public static async getDealsToUser(
    userId: number,
    pageSize: number,
    page: number
  ): Promise<ApiResponse<Common.PaginatedResult<Deal.Deal>>> {
    return await UserApi.get<Common.PaginatedResult<Deal.Deal>>(
      `/user/${userId}/deals/to?pageSize=${pageSize}&pageNumber=${page}`,
      true
    );
  }
}