namespace Chess.GameSystems {
    public interface ChessPlayer {
        int getId();
        bool isLoser();
        int getPieceNum();
        void setPieceNum(int num);
        void kingTaken();
    }
}