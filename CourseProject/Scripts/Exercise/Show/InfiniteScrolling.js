(function() {
  $(function() {
    return $(document).ready(function() {
      var blockNumber, inProgress;
      blockNumber = 2;
      $("#InfinityBlockNumber")[0].value = blockNumber;
      inProgress = false;
      return $(window).scroll(function() {
        if ($(window).scrollTop() === $(document).height() - $(window).height() && !inProgress) {
          blockNumber = parseInt($("#InfinityBlockNumber")[0].value);
          inProgress = true;
          $("#loadingDiv").show();
          return $.post("/Exercise/InfinateScroll", {
            "blockNumber": blockNumber,
            "id": $('#Id')[0].value
          }, function(data) {
            blockNumber = blockNumber + 1;
            $("#commentsList").append(data.HTMLString);
            $("#loadingDiv").hide();
            $("#InfinityBlockNumber")[0].value = blockNumber;
            return inProgress = false;
          });
        }
      });
    });
  });

}).call(this);

//# sourceMappingURL=InfiniteScrolling.js.map
