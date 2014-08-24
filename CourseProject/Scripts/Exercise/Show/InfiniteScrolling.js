(function() {
  $(function() {
    return $(document).ready(function() {
      var NoMoreData, blockNumber, inProgress;
      blockNumber = 2;
      NoMoreData = false;
      inProgress = false;
      return $(window).scroll(function() {
        if ($(window).scrollTop() === $(document).height() - $(window).height() && !NoMoreData && !inProgress) {
          inProgress = true;
          $("#loadingDiv").show();
          return $.post("/Exercise/InfinateScroll", {
            "blockNumber": blockNumber,
            "id": $('#Id')[0].value
          }, function(data) {
            blockNumber = blockNumber + 1;
            NoMoreData = data.NoMoreData;
            $("#commentsList").append(data.HTMLString);
            $("#loadingDiv").hide();
            return inProgress = false;
          });
        }
      });
    });
  });

}).call(this);

//# sourceMappingURL=InfiniteScrolling.js.map
